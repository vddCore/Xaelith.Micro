namespace Xaelith.Micro.Infrastructure.Utilities;

public static class FileSystemUtility
{
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
}