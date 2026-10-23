using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using System.Threading.RateLimiting;
using PayOS;
using PayOS.Models;
using PayOS.Models.V2.PaymentRequests;
using PayOS.Models.Webhooks;

var builder = WebApplication.CreateBuilder(args);
string localSettingsPath = LocalPaymentsSettings.EnsureApiSettingsFile(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile(localSettingsPath, optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
PayOsConfiguration.Initialize(builder.Configuration);
if (builder.Configuration["Payments:ApiListenUrl"] is { Length: > 0 } apiListenUrl &&
    string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_URLS")))
    builder.WebHost.UseUrls(apiListenUrl);
if (builder.Configuration.GetConnectionString("CinemaDb") is { Length: > 0 } connectionString)
    DataProcesser.ConnectionString = connectionString;
builder.Services.AddHostedService<PendingPaymentExpiryWorker>();
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("payment-session", context => RateLimitPartition.GetFixedWindowLimiter(
        context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            AutoReplenishment = true
        }));
});
var app = builder.Build();
app.UseRouting();
app.UseRateLimiter();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapPost("/api/auth/session", (HttpRequest http, LoginRequest request) =>
{
    if (!HasClientKey(http)) return Results.Unauthorized();
    if (!Function.ValidateCredentials(request.Username.Trim(), request.Password, out string role, out _))
        return Results.Unauthorized();
    if (AccessRoles.Normalize(role) != AccessRoles.Cashier)
        return Results.StatusCode(StatusCodes.Status403Forbidden);
    try
    {
        return Results.Ok(new
        {
            token = PaymentSessionTokens.Issue(request.Username.Trim()),
            expiresAtUtc = DateTimeOffset.UtcNow.AddHours(8)
        });
    }
    catch (InvalidOperationException ex)
    {
        app.Logger.LogError(ex, "Payment session signing is not configured");
        return Results.Problem("Dịch vụ phiên thanh toán chưa được cấu hình.", statusCode: StatusCodes.Status503ServiceUnavailable);
    }
}).RequireRateLimiting("payment-session");
app.MapGet("/payos/return", () => Results.Content(
    "<!doctype html><html lang='vi'><meta charset='utf-8'><title>Đã quay lại rạp phim</title>" +
    "<body style='font-family:Segoe UI,sans-serif;padding:2rem'><h2>Đã quay lại NHH Cinema</h2>" +
    "<p>Vui lòng quay lại ứng dụng bán vé để xem trạng thái thanh toán.</p></body></html>", "text/html; charset=utf-8"));
app.MapGet("/payos/cancel", () => Results.Content(
    "<!doctype html><html lang='vi'><meta charset='utf-8'><title>Thanh toán đã hủy</title>" +
    "<body style='font-family:Segoe UI,sans-serif;padding:2rem'><h2>Thanh toán chưa hoàn tất</h2>" +
    "<p>Vui lòng quay lại ứng dụng bán vé.</p></body></html>", "text/html; charset=utf-8"));

app.MapPost("/api/payments", async (HttpRequest http, CreateSaleRequest request) =>
{
    string? authenticatedUser = GetSessionUser(http);
    if (authenticatedUser is null) return Results.Unauthorized();
    if (!PayOsConfiguration.IsConfigured) return Results.Problem("PayOS chưa được cấu hình trên máy chủ.", statusCode: StatusCodes.Status503ServiceUnavailable);
    try
    {
        var payOs = PayOsConfiguration.CreateClient();
        string returnUrl = RequiredSetting("PAYOS_RETURN_URL", "Payments:ReturnUrl");
        string cancelUrl = RequiredSetting("PAYOS_CANCEL_URL", "Payments:CancelUrl");
        var sale = SalesService.CreatePendingPayOsSale(request.Sale with { TenDangNhapNguoiBan = authenticatedUser });
        CreatePaymentLinkResponse? createdPayment = null;
        try
        {
            createdPayment = await payOs.PaymentRequests.CreateAsync(new CreatePaymentLinkRequest
            {
                OrderCode = sale.PayOSOrderCode!.Value,
                Amount = checked((int)sale.TongTien),
                Description = $"Ve phim {sale.MaHD}"[..Math.Min($"Ve phim {sale.MaHD}".Length, 25)],
                ExpiredAt = DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds(),
                ReturnUrl = returnUrl,
                CancelUrl = cancelUrl
            });
            SalesService.SetPayOsLink(sale.PayOSOrderCode.Value, createdPayment.PaymentLinkId);
            return Results.Ok(new
            {
                sale.MaHD,
                OrderCode = sale.PayOSOrderCode,
                sale.TongTien,
                createdPayment.PaymentLinkId,
                createdPayment.CheckoutUrl,
                createdPayment.QrCode,
                Sale = sale
            });
        }
        catch
        {
            if (createdPayment is not null)
            {
                try { await payOs.PaymentRequests.CancelAsync(createdPayment.PaymentLinkId, "Unable to persist PayOS link"); }
                catch (Exception cancelError) { app.Logger.LogWarning(cancelError, "Could not cancel orphan PayOS link {PaymentLinkId}", createdPayment.PaymentLinkId); }
            }
            SalesService.CancelPendingPayOs(sale.PayOSOrderCode!.Value, "Không thể khởi tạo thanh toán PayOS");
            throw;
        }
    }
    catch (InvalidOperationException ex) { return Results.BadRequest(new { error = ex.Message }); }
    catch (UnauthorizedAccessException) { return Results.StatusCode(StatusCodes.Status403Forbidden); }
    catch (Exception ex) { app.Logger.LogError(ex, "Failed to create PayOS payment"); return Results.Problem("Không thể tạo giao dịch thanh toán."); }
});

app.MapGet("/api/payments/{orderCode:long}", (HttpRequest http, long orderCode) =>
{
    string? authenticatedUser = GetSessionUser(http);
    if (authenticatedUser is null) return Results.Unauthorized();
    try { return Results.Ok(new { orderCode, status = SalesService.GetPaymentStatus(orderCode, authenticatedUser) }); }
    catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); }
});

app.MapPost("/api/payments/{orderCode:long}/cancel", async (HttpRequest http, long orderCode) =>
{
    string? authenticatedUser = GetSessionUser(http);
    if (authenticatedUser is null) return Results.Unauthorized();
    try
    {
        string localStatus = SalesService.GetPaymentStatus(orderCode, authenticatedUser);
        if (localStatus != "Pending") return Results.Ok(new { status = localStatus });
        if (!PayOsConfiguration.IsConfigured) return Results.Problem("PayOS chưa được cấu hình trên máy chủ.", statusCode: StatusCodes.Status503ServiceUnavailable);
        var payOs = PayOsConfiguration.CreateClient();
        var payment = await payOs.PaymentRequests.GetAsync(orderCode);
        if (!string.IsNullOrWhiteSpace(payment.Id))
            await payOs.PaymentRequests.CancelAsync(payment.Id, "Cashier closed payment window");
        SalesService.CancelPendingPayOs(orderCode, "Giao dịch bị hủy bởi thu ngân", authenticatedUser);
        return Results.Ok(new { status = SalesService.GetPaymentStatus(orderCode, authenticatedUser) });
    }
    catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); }
    catch (Exception ex) { app.Logger.LogError(ex, "Failed to cancel PayOS order {OrderCode}", orderCode); return Results.Problem("Không thể hủy giao dịch đang chờ."); }
});

app.MapPost("/webhooks/payos", async (Webhook webhook) =>
{
    try
    {
        if (!PayOsConfiguration.IsConfigured) return Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
        var payOs = PayOsConfiguration.CreateClient();
        var data = await payOs.Webhooks.VerifyAsync(webhook);
        // PayOS webhook callbacks may omit the optional top-level Success field.
        // The SDK has already verified the signature; use the signed response codes instead.
        if (!string.Equals(webhook.Code, "00", StringComparison.Ordinal) ||
            !string.Equals(data.Code, "00", StringComparison.Ordinal) ||
            !string.Equals(data.Currency, "VND", StringComparison.OrdinalIgnoreCase))
            return Results.BadRequest(new { error = "Webhook không báo giao dịch thanh toán thành công bằng VND." });

        // payOS sends this signed sample payment when a merchant confirms its webhook URL.
        // Acknowledge only the documented sample; it must never update a cinema invoice.
        if (data.OrderCode == 123 && data.Amount == 3000 &&
            string.Equals(data.PaymentLinkId, "124c33293c43417ab7879e14c8d9eb18", StringComparison.Ordinal))
            return Results.Ok(new { success = true });

        if (!SalesService.CompletePayOsPayment(data.OrderCode, checked((int)data.Amount), data.PaymentLinkId))
            return Results.Conflict(new { error = "Không thể xác nhận giao dịch." });
        return Results.Ok(new { success = true });
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "PayOS webhook verification or processing failed");
        return Results.BadRequest(new { error = "Webhook không hợp lệ hoặc không khớp giao dịch." });
    }
});

app.Run();

bool HasClientKey(HttpRequest request)
{
    string expected = PayOsConfiguration.GetSetting("PAYMENTS_CLIENT_KEY", "Payments:ClientKey") ?? string.Empty;
    return expected.Length >= 32 && request.Headers.TryGetValue("X-Payments-Client-Key", out var supplied) &&
        System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(
            System.Text.Encoding.UTF8.GetBytes(expected), System.Text.Encoding.UTF8.GetBytes(supplied.ToString()));
}

string? GetSessionUser(HttpRequest request)
{
    string token = request.Headers["Authorization"].ToString();
    if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) token = token[7..].Trim();
    return PaymentSessionTokens.TryValidate(token, out string username) ? username : null;
}

string RequiredSetting(string name, string configPath) => PayOsConfiguration.GetSetting(name, configPath)
    ?? throw new InvalidOperationException($"Server configuration {name} is missing.");

public sealed record CreateSaleRequest(SaleDraft Sale);
public sealed record LoginRequest(string Username, string Password);

public sealed class PendingPaymentExpiryWorker(ILogger<PendingPaymentExpiryWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                if (!PayOsConfiguration.IsConfigured) continue;
                var payOs = PayOsConfiguration.CreateClient();
                foreach (long orderCode in SalesService.GetExpiredPayOsOrders())
                {
                    try
                    {
                        var payment = await payOs.PaymentRequests.GetAsync(orderCode);
                        string remoteStatus = payment.Status.ToString();
                        if (remoteStatus.Contains("PAID", StringComparison.OrdinalIgnoreCase))
                        {
                            SalesService.CompletePayOsPayment(orderCode, checked((int)payment.AmountPaid), payment.Id);
                            continue;
                        }
                        if (remoteStatus.Contains("PENDING", StringComparison.OrdinalIgnoreCase))
                            await payOs.PaymentRequests.CancelAsync(payment.Id, "Payment expired");
                        if (remoteStatus.Contains("PENDING", StringComparison.OrdinalIgnoreCase) ||
                            remoteStatus.Contains("CANCEL", StringComparison.OrdinalIgnoreCase) ||
                            remoteStatus.Contains("EXPIRE", StringComparison.OrdinalIgnoreCase))
                            SalesService.CancelPendingPayOs(orderCode, "Giao dịch hết hạn");
                        else
                            logger.LogWarning("PayOS order {OrderCode} has unexpected remote status {Status}; holds retained", orderCode, remoteStatus);
                    }
                    catch (Exception ex) { logger.LogWarning(ex, "Could not expire PayOS order {OrderCode}", orderCode); }
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception ex) { logger.LogError(ex, "Payment expiry scan failed"); }
        }
    }
}

public static class PayOsConfiguration
{
    private static IConfiguration? _configuration;

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;
        SetEnvironmentFallback("PAYOS_CLIENT_ID", configuration["PayOS:ClientId"]);
        SetEnvironmentFallback("PAYOS_API_KEY", configuration["PayOS:ApiKey"]);
        SetEnvironmentFallback("PAYOS_CHECKSUM_KEY", configuration["PayOS:ChecksumKey"]);
    }

    public static bool IsConfigured => new[]
    {
        GetSetting("PAYOS_CLIENT_ID", "PayOS:ClientId"),
        GetSetting("PAYOS_API_KEY", "PayOS:ApiKey"),
        GetSetting("PAYOS_CHECKSUM_KEY", "PayOS:ChecksumKey")
    }.All(value => !string.IsNullOrWhiteSpace(value));

    public static string? GetSetting(string environmentName, string configurationPath) =>
        Environment.GetEnvironmentVariable(environmentName) is { Length: > 0 } value
            ? value
            : _configuration?[configurationPath];

    private static void SetEnvironmentFallback(string name, string? value)
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(name)) && !string.IsNullOrWhiteSpace(value))
            Environment.SetEnvironmentVariable(name, value);
    }

    public static PayOSClient CreateClient()
    {
        if (!IsConfigured) throw new InvalidOperationException("PayOS credentials are not configured on the server.");
        SetEnvironmentFallback("PAYOS_CLIENT_ID", _configuration?["PayOS:ClientId"]);
        SetEnvironmentFallback("PAYOS_API_KEY", _configuration?["PayOS:ApiKey"]);
        SetEnvironmentFallback("PAYOS_CHECKSUM_KEY", _configuration?["PayOS:ChecksumKey"]);
        return new PayOSClient();
    }
}
