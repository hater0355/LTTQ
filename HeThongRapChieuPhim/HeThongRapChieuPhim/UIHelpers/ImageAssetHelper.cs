using System.Drawing;
using System.IO;

namespace HeThongRapChieuPhim.UIHelpers;

public static class ImageAssetHelper
{
    public static string? ResolvePath(string? relativeOrFullPath)
    {
        if (string.IsNullOrWhiteSpace(relativeOrFullPath)) return null;

        string path = relativeOrFullPath.Trim().Replace('/', Path.DirectorySeparatorChar);
        var candidates = new List<string>();

        if (Path.IsPathRooted(path))
        {
            candidates.Add(path);
        }
        else
        {
            string baseDirectory = AppContext.BaseDirectory;
            candidates.Add(Path.Combine(baseDirectory, path));
            if (!path.StartsWith($"Assets{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                candidates.Add(Path.Combine(baseDirectory, "Assets", path));

            string currentDirectory = Directory.GetCurrentDirectory();
            candidates.Add(Path.Combine(currentDirectory, path));
            if (!path.StartsWith($"Assets{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                candidates.Add(Path.Combine(currentDirectory, "Assets", path));
        }

        return candidates.FirstOrDefault(File.Exists);
    }

    public static Image? Load(string? relativeOrFullPath)
    {
        string? path = ResolvePath(relativeOrFullPath);
        if (path is null) return null;

        try
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            using var source = Image.FromStream(stream);
            return new Bitmap(source);
        }
        catch
        {
            return null;
        }
    }
}
