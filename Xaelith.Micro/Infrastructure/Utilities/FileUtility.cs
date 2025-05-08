namespace Xaelith.Micro.Infrastructure.Utilities;

public static class FileUtility
{
    private static Dictionary<string, byte[]> _knownFileTypes = new()
    {
        { "bmp", new byte[] { 0x42, 0x4D } },
        { "gif87a", new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 } },
        { "gif89a", new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 } },
        { "png", new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }},
        { "jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
        { "tiffI", new byte[] { 0x49, 0x49, 0x2A, 0x00 } },
        { "tiffM", new byte[] { 0x4D, 0x4D, 0x00, 0x2A } }
    };

    public static string FormatSize(double bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
        int order = 0;

        while (bytes >= 1024 && order < sizes.Length - 1)
        {
            bytes /= 1024;
            order++;
        }

        return $"{Math.Round(bytes)}{sizes[order]}";
    }

    public static string? GetImageType(string file)
    {
        byte[] buffer = new byte[8];
        
        try
        {
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length > buffer.Length)
                {
                    fs.ReadExactly(buffer, 0, buffer.Length);
                }
            }

            foreach (var (typeName, header) in _knownFileTypes)
            {
                if (buffer.StartsWith(header))
                    return typeName;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}