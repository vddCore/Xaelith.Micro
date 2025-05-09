namespace Xaelith.Micro.Infrastructure.Utilities;

using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

public static class NavigationManagerExtensions
{
    public static NameValueCollection QueryString(this NavigationManager navigationManager)
        => HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);

    public static string QueryString(this NavigationManager navigationManager, string key)
        => navigationManager.QueryString()[key] 
        ?? string.Empty;
    
    public static string GetUriWithQueryParameter(this NavigationManager navigationManager, string key, object? value)
    {
        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        var baseUri = uri.GetLeftPart(UriPartial.Path);
        var query = QueryHelpers.ParseQuery(uri.Query);
        var dict = query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

        dict[key] = value?.ToString() ?? string.Empty;

        var newQuery = QueryHelpers.AddQueryString(baseUri, dict!);

        return newQuery;
    }
}