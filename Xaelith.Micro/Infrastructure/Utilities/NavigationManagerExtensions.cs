namespace Xaelith.Micro.Infrastructure.Utilities;

using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Components;

public static class NavigationManagerExtensions
{
    public static NameValueCollection QueryString(this NavigationManager navigationManager)
        => HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);

    public static string QueryString(this NavigationManager navigationManager, string key)
        => navigationManager.QueryString()[key] 
        ?? string.Empty;
}