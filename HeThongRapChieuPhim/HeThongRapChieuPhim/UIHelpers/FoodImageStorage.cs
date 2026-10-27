using System.Drawing;

namespace HeThongRapChieuPhim.UIHelpers;

/// <summary>Stores selected food images in a stable, relative asset location.</summary>
public static class FoodImageStorage
{
    private const string RelativeDirectory = "Assets/Food";
    private static readonly HashSet<string> SupportedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".bmp", ".gif"
    };

    public static string Store(string sourcePath, string foodCode)
    {
        if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
            throw new FileNotFoundException("Không tìm thấy tệp ảnh đã chọn.", sourcePath);

        string extension = Path.GetExtension(sourcePath);
        if (!SupportedExtensions.Contains(extension))
            throw new InvalidOperationException("Ảnh phải có định dạng JPG, PNG, BMP hoặc GIF.");

        // Decode once before copying so a renamed non-image file is not accepted.
        using (Image.FromFile(sourcePath)) { }

        string assetRoot = FindProjectRoot() ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NHHCinema");
        string destinationDirectory = Path.Combine(assetRoot, "Assets", "Food");
        Directory.CreateDirectory(destinationDirectory);

        string safeCode = new(foodCode.Where(char.IsLetterOrDigit).ToArray());
        if (safeCode.Length == 0) safeCode = "Food";
        string fileName = $"{safeCode}_{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        File.Copy(sourcePath, Path.Combine(destinationDirectory, fileName));

        return $"{RelativeDirectory}/{fileName}";
    }

    private static string? FindProjectRoot()
    {
        foreach (string startPath in new[] { AppContext.BaseDirectory, Directory.GetCurrentDirectory() })
        {
            DirectoryInfo? current;
            try { current = new DirectoryInfo(Path.GetFullPath(startPath)); }
            catch { continue; }

            while (current is not null)
            {
                if (File.Exists(Path.Combine(current.FullName, "HeThongRapChieuPhim.csproj")))
                    return current.FullName;
                current = current.Parent;
            }
        }

        return null;
    }
}
