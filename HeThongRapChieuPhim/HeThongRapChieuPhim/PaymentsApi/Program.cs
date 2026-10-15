using HeThongRapChieuPhim.Classes;
using PayOS;
using PayOS.Models;
using PayOS.Models.V2.PaymentRequests;
using PayOS.Models.Webhooks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<PendingPaymentExpiryWorker>();
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapPost("/api/payments", async (HttpRequest http, CreateSaleRequest request) =>
{
    if (!HasClientKey(http)) return Results.Unauthorized();
    if (!PayOsConfiguration.IsConfigured) return Results.Problem("PayOS chưa được cấu hình trên máy chủ.", statusCode: StatusCodes.Status503ServiceUnavailable);
    try
    {
        var payOs = PayOsConfiguration.CreateClient();
        string returnUrl = RequiredSetting("PAYOS_RETURN_URL");
        string cancelUrl = RequiredSetting("PAYOS_CANCEL_URL");
        var sale = SalesService.CreatePendingPayOsSale(request.Sale);
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
    if (!HasClientKey(http)) return Results.Unauthorized();
    try { return Results.Ok(new { orderCode, status = SalesService.GetPaymentStatus(orderCode) }); }
    catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); }
});

app.MapPost("/api/payments/{orderCode:long}/cancel", async (HttpRequest http, long orderCode) =>
{
    if (!HasClientKey(http)) return Results.Unauthorized();
    try
    {
        string localStatus = SalesService.GetPaymentStatus(orderCode);
        if (localStatus != "Pending") return Results.Ok(new { status = localStatus });
        if (!PayOsConfiguration.IsConfigured) return Results.Problem("PayOS chưa được cấu hình trên máy chủ.", statusCode: StatusCodes.Status503ServiceUnavailable);
        var payOs = PayOsConfiguration.CreateClient();
        var payment = await payOs.PaymentRequests.GetAsync(orderCode);
        if (!string.IsNullOrWhiteSpace(payment.Id))
            await payOs.PaymentRequests.CancelAsync(payment.Id, "Cashier closed payment window");
        SalesService.CancelPendingPayOs(orderCode, "Giao dịch bị hủy bởi thu ngân");
        return Results.Ok(new { status = SalesService.GetPaymentStatus(orderCode) });
    }
    catch (Exception ex) { app.Logger.LogError(ex, "Failed to cancel PayOS order {OrderCode}", orderCode); return Results.Problem("Không thể hủy giao dịch đang chờ."); }
});

app.MapPost("/webhooks/payos", async (Webhook webhook) =>
{
    try
    {
        if (!PayOsConfiguration.IsConfigured) return Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
        var payOs = PayOsConfiguration.CreateClient();
        var data = await payOs.Webhooks.VerifyAsync(webhook);
        if (!webhook.Success || !string.Equals(webhook.Code, "00", StringComparison.Ordinal) ||
            !string.Equals(data.Code, "00", StringComparison.Ordinal) ||
            !string.Equals(data.Currency, "VND", StringComparison.OrdinalIgnoreCase))
            return Results.BadRequest(new { error = "Webhook không báo giao dịch thanh toán thành công bằng VND." });

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
    string expected = Environment.GetEnvironmentVariable("PAYMENTS_CLIENT_KEY") ?? string.Empty;
    return expected.Length >= 32 && request.Headers.TryGetValue("X-Payments-Client-Key", out var supplied) &&
        System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(
            System.Text.Encoding.UTF8.GetBytes(expected), System.Text.Encoding.UTF8.GetBytes(supplied.ToString()));
}

string RequiredSetting(string name) => Environment.GetEnvironmentVariable(name)
    ?? throw new InvalidOperationException($"Server configuration {name} is missing.");

public sealed record CreateSaleRequest(SaleDraft Sale);

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
    public static bool IsConfigured => new[] { "PAYOS_CLIENT_ID", "PAYOS_API_KEY", "PAYOS_CHECKSUM_KEY" }
        .All(name => !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(name)));

    public static PayOSClient CreateClient()
    {
        if (!IsConfigured) throw new InvalidOperationException("PayOS credentials are not configured on the server.");
        return new PayOSClient();
    }
}
