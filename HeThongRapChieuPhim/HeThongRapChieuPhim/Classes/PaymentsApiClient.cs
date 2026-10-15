using System.Net.Http.Json;
using System.Text.Json;

namespace HeThongRapChieuPhim.Classes;

public sealed record PayOsCheckout(long OrderCode, string MaHD, decimal TongTien, string CheckoutUrl, string QrCode, SaleResult Sale);

/// <summary>WinForms client for the separately hosted payment API; PayOS secrets never enter the desktop process.</summary>
public static class PaymentsApiClient
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(20) };
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

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

    private static HttpResponseMessage Send(HttpMethod method, string relativeUrl, HttpContent? content = null)
    {
        string baseValue = Environment.GetEnvironmentVariable("PAYMENTS_API_URL") ?? string.Empty;
        if (!Uri.TryCreate(baseValue, UriKind.Absolute, out var baseUri) || baseUri.Scheme is not ("https" or "http"))
            throw new InvalidOperationException("PAYMENTS_API_URL chưa được cấu hình bằng địa chỉ API hợp lệ.");
        string key = Environment.GetEnvironmentVariable("PAYMENTS_CLIENT_KEY") ?? string.Empty;
        if (key.Length < 32) throw new InvalidOperationException("Thiếu PAYMENTS_CLIENT_KEY có tối thiểu 32 ký tự.");

        using var request = new HttpRequestMessage(method, new Uri(new Uri(baseUri.ToString().TrimEnd('/') + "/"), relativeUrl)) { Content = content };
        request.Headers.Add("X-Payments-Client-Key", key);
        var response = Http.Send(request);
        if (response.IsSuccessStatusCode) return response;
        string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        string message = $"Payments API trả về {(int)response.StatusCode}: {body}";
        response.Dispose();
        throw new InvalidOperationException(message);
    }

    private sealed record CreatePaymentRequest(SaleDraft Sale);
}
