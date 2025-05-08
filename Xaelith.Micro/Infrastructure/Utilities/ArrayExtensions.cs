namespace Xaelith.Micro.Infrastructure.Utilities;

public static class ArrayExtensions
{
    public static bool StartsWith(this byte[] a, byte[] b)
    {
        if (a.Length < b.Length)
            return false;

        for (int i = 0; i < b.Length; i++)
        {
            if (a[i] != b[i])
                return false;
        }

        return true;
    }
}