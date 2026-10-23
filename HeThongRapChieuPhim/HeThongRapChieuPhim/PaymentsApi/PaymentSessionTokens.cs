using System.Security.Cryptography;
using System.Text;

public static class PaymentSessionTokens
{
    private static readonly TimeSpan Lifetime = TimeSpan.FromHours(8);

    public static string Issue(string username)
    {
        byte[] key = GetSigningKey();
        long expiresAt = DateTimeOffset.UtcNow.Add(Lifetime).ToUnixTimeSeconds();
        string encodedUser = Base64UrlEncode(Encoding.UTF8.GetBytes(username));
        string payload = $"{encodedUser}.{expiresAt}";
        string signature = Base64UrlEncode(HMACSHA256.HashData(key, Encoding.UTF8.GetBytes(payload)));
        return $"{payload}.{signature}";
    }

    public static bool TryValidate(string? token, out string username)
    {
        username = string.Empty;
        if (string.IsNullOrWhiteSpace(token)) return false;
        string[] parts = token.Split('.');
        if (parts.Length != 3 || !long.TryParse(parts[1], out long expiresAt) ||
            expiresAt <= DateTimeOffset.UtcNow.ToUnixTimeSeconds()) return false;

        try
        {
            string payload = $"{parts[0]}.{parts[1]}";
            byte[] expected = HMACSHA256.HashData(GetSigningKey(), Encoding.UTF8.GetBytes(payload));
            byte[] supplied = Base64UrlDecode(parts[2]);
            if (supplied.Length != expected.Length || !CryptographicOperations.FixedTimeEquals(expected, supplied))
                return false;

            string decodedUsername = Encoding.UTF8.GetString(Base64UrlDecode(parts[0]));
            if (string.IsNullOrWhiteSpace(decodedUsername) || decodedUsername.Length > 50) return false;
            username = decodedUsername;
            return true;
        }
        catch (Exception ex) when (ex is FormatException or CryptographicException or ArgumentException)
        {
            return false;
        }
    }

    private static byte[] GetSigningKey()
    {
        string? value = PayOsConfiguration.GetSetting("PAYMENTS_SESSION_SIGNING_KEY", "Payments:SessionSigningKey");
        if (string.IsNullOrWhiteSpace(value) || Encoding.UTF8.GetByteCount(value) < 32)
            throw new InvalidOperationException("Payment session signing key is not configured.");
        return Encoding.UTF8.GetBytes(value);
    }

    private static string Base64UrlEncode(byte[] bytes) => Convert.ToBase64String(bytes)
        .TrimEnd('=').Replace('+', '-').Replace('/', '_');

    private static byte[] Base64UrlDecode(string value)
    {
        string base64 = value.Replace('-', '+').Replace('_', '/');
        base64 += new string('=', (4 - base64.Length % 4) % 4);
        return Convert.FromBase64String(base64);
    }
}
