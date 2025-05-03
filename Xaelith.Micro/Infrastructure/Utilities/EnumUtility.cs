namespace Xaelith.Micro.Infrastructure.Utilities;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class EnumUtility
{
    public static string GetDisplayName(Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? value.ToString();
    }
}