namespace Xaelith.Micro.Infrastructure.Utilities;

using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

public static class RenderModeSelector
{
    private static readonly Dictionary<string, IComponentRenderMode?> _routeSpecificRenderModes = new();

    public static IComponentRenderMode Default { get; set; } = RenderMode.InteractiveServer;

    public static IComponentRenderMode? GetRenderMode(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;

        if (string.IsNullOrWhiteSpace(path))
            return Default;
        
        var match = _routeSpecificRenderModes.Keys.FirstOrDefault(
            x => x.StartsWith(path, StringComparison.OrdinalIgnoreCase)
        );
        
        return match is not null && _routeSpecificRenderModes.TryGetValue(
            match, 
            out var renderMode
        ) ? renderMode : Default;
    }
    
    public static void RegisterRenderModeOverrides()
    {
        var componentTypes = Assembly
            .GetExecutingAssembly()
            .ExportedTypes
            .Where(t => typeof(IComponent).IsAssignableFrom(t));

        foreach (var type in componentTypes)
        {
            var routeAttrs = type.GetCustomAttributes(
                typeof(RouteAttribute),
                false
            ).Cast<RouteAttribute>();
        
            var hasStaticAttr = type.GetCustomAttributes(
                typeof(RequiresStaticRenderingAttribute), 
                false
            ).Any();

            foreach (var route in routeAttrs)
            {
                if (string.IsNullOrWhiteSpace(route.Template))
                    continue;

                _routeSpecificRenderModes[route.Template] = hasStaticAttr
                    ? null
                    : RenderMode.InteractiveServer;
            }
        }
    }
}