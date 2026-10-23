using System.Net.Http.Json;
using System.Text.Json;

namespace HeThongRapChieuPhim.Classes;

public sealed record PayOsCheckout(long OrderCode, string MaHD, decimal TongTien, string CheckoutUrl, string QrCode, SaleResult Sale);

/// <summary>WinForms client for the separately hosted payment API; PayOS secrets never enter the desktop process.</summary>
public static class PaymentsApiClient
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(20) };
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static string? _sessionToken;

    public static void ClearSession() => _sessionToken = null;

    public static void CreateSession(string username, string password)
    {
        using var response = Send(HttpMethod.Post, "api/auth/session",
            JsonContent.Create(new LoginRequest(username, password), options: JsonOptions), useClientKey: true);
        using var json = JsonDocument.Parse(response.Content.ReadAsStream());
        _sessionToken = json.RootElement.GetProperty("token").GetString()
            ?? throw new InvalidOperationException("Payments API không trả phiên đăng nhập.");
    }

    public static PayOsCheckout CreateCheckout(SaleDraft sale)
    {
        using var response = Send(HttpMethod.Post, "api/payments", JsonContent.Create(new CreatePaymentRequest(sale), options: JsonOptions));
        using var json = JsonDocument.Parse(response.Content.ReadAsStream());
        var root = json.RootElement;
        return new PayOsCheckout(root.GetProperty("orderCode").GetInt64(), root.GetProperty("maHD").GetString()!,
            root.GetProperty("tongTien").GetDecimal(), root.GetProperty("checkoutUrl").GetString()!, root.GetProperty("qrCode").GetString()!,
            JsonSerializer.Deserialize<SaleResult>(root.GetProperty("sale").GetRawText(), JsonOptions)
                ?? throw new InvalidOperationException("API không trả thông tin hóa đơn."));
    }

    public static string GetStatus(long orderCode)
    {
        using var response = Send(HttpMethod.Get, $"api/payments/{orderCode}");
        using var json = JsonDocument.Parse(response.Content.ReadAsStream());
        return json.RootElement.GetProperty("status").GetString() ?? "Unknown";
    }

    public static void Cancel(long orderCode)
    {
        using var response = Send(HttpMethod.Post, $"api/payments/{orderCode}/cancel");
    }

    private static HttpResponseMessage Send(HttpMethod method, string relativeUrl, HttpContent? content = null, bool useClientKey = false)
    {
        string baseValue = Environment.GetEnvironmentVariable("PAYMENTS_API_URL") ??
            LocalPaymentsSettings.ReadValue("Payments", "ApiUrl") ?? "http://localhost:5080";
        if (!Uri.TryCreate(baseValue, UriKind.Absolute, out var baseUri) || baseUri.Scheme is not ("https" or "http"))
            throw new InvalidOperationException("PAYMENTS_API_URL chưa được cấu hình bằng địa chỉ API hợp lệ.");
        using var request = new HttpRequestMessage(method, new Uri(new Uri(baseUri.ToString().TrimEnd('/') + "/"), relativeUrl)) { Content = content };
        if (useClientKey)
        {
            string key = Environment.GetEnvironmentVariable("PAYMENTS_CLIENT_KEY") ??
                LocalPaymentsSettings.ReadValue("Payments", "ClientKey") ?? string.Empty;
            if (key.Length < 32) throw new InvalidOperationException("Thiếu PAYMENTS_CLIENT_KEY có tối thiểu 32 ký tự.");
            request.Headers.Add("X-Payments-Client-Key", key);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(_sessionToken))
                throw new InvalidOperationException("Chưa có phiên thanh toán hợp lệ. Hãy đăng nhập lại khi Payments API đang chạy.");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _sessionToken);
        }
        var response = Http.Send(request);
        if (response.IsSuccessStatusCode) return response;
        string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        string message = $"Payments API trả về {(int)response.StatusCode}: {body}";
        response.Dispose();
        throw new InvalidOperationException(message);
    }

    private sealed record CreatePaymentRequest(SaleDraft Sale);
    private sealed record LoginRequest(string Username, string Password);
}
