using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeThongRapChieuPhim.Classes;

public static class LocalPaymentsSettings
{
    private const string SettingsFileName = "appsettings.Local.json";

    public static string EnsureApiSettingsFile(string contentRoot)
    {
        string apiDirectory = FindApiProjectDirectory(contentRoot);
        string path = Path.Combine(apiDirectory, SettingsFileName);
        if (!File.Exists(Path.Combine(apiDirectory, "PaymentsApi.csproj")))
            return path;

        if (File.Exists(path))
        {
            EnsureClientKey(path);
            return path;
        }

        var settings = new
        {
            PayOS = new { ClientId = "", ApiKey = "", ChecksumKey = "" },
            Payments = new
            {
                ClientKey = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)),
                SessionSigningKey = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)),
                ApiUrl = "http://localhost:5080",
                ApiListenUrl = "http://localhost:5080",
                ReturnUrl = "http://localhost:5080/payos/return",
                CancelUrl = "http://localhost:5080/payos/cancel"
            },
            ConnectionStrings = new
            {
                CinemaDb = @"Server=.;Database=NHHCinemaDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5;"
            }
        };

        File.WriteAllText(path, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
        return path;
    }

    private static void EnsureClientKey(string path)
    {
        try
        {
            var root = JsonNode.Parse(File.ReadAllText(path)) as JsonObject;
            if (root is null) return;
            var payments = root["Payments"] as JsonObject;
            if (payments is null)
            {
                payments = new JsonObject();
                root["Payments"] = payments;
            }
            bool changed = false;
            if (string.IsNullOrWhiteSpace(payments["ClientKey"]?.GetValue<string>()))
            {
                payments["ClientKey"] = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
                changed = true;
            }
            if (string.IsNullOrWhiteSpace(payments["SessionSigningKey"]?.GetValue<string>()))
            {
                payments["SessionSigningKey"] = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
                changed = true;
            }
            if (changed) File.WriteAllText(path, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException or InvalidOperationException)
        {
            // The API can still start with environment-based settings if the local file is read-only or malformed.
        }
    }

    public static string? ReadValue(string section, string name)
    {
        string? path = FindSettingsFile();
        if (path is null) return null;

        try
        {
            using var json = JsonDocument.Parse(File.ReadAllText(path));
            if (!json.RootElement.TryGetProperty(section, out var sectionElement) ||
                !sectionElement.TryGetProperty(name, out var valueElement))
                return null;
            return valueElement.ValueKind == JsonValueKind.String ? valueElement.GetString() : null;
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            return null;
        }
    }

    public static string FindApiProjectDirectory(string startPath)
    {
        foreach (string root in new[] { startPath, AppContext.BaseDirectory })
        {
            DirectoryInfo? current;
            try { current = new DirectoryInfo(Path.GetFullPath(root)); }
            catch { continue; }

            while (current is not null)
            {
                if (File.Exists(Path.Combine(current.FullName, "PaymentsApi.csproj")))
                    return current.FullName;
                string nestedApi = Path.Combine(current.FullName, "PaymentsApi");
                if (File.Exists(Path.Combine(nestedApi, "PaymentsApi.csproj")))
                    return nestedApi;
                current = current.Parent;
            }
        }
        return Path.GetFullPath(startPath);
    }

    private static string? FindSettingsFile()
    {
        string[] roots = [Environment.CurrentDirectory, AppContext.BaseDirectory];
        foreach (string root in roots)
        {
            DirectoryInfo? current;
            try { current = new DirectoryInfo(Path.GetFullPath(root)); }
            catch { continue; }

            while (current is not null)
            {
                string direct = Path.Combine(current.FullName, SettingsFileName);
                if (File.Exists(direct)) return direct;

                string nestedApi = Path.Combine(current.FullName, "PaymentsApi", SettingsFileName);
                if (File.Exists(nestedApi)) return nestedApi;
                current = current.Parent;
            }
        }
        return null;
    }
}
