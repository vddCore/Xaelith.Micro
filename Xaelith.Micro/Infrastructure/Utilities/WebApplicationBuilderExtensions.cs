namespace Xaelith.Micro.Infrastructure.Utilities;

using System.Reflection;
using Xaelith.Micro.Infrastructure.ServiceModel;

public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection AttachXaelithServices(
        this IServiceCollection services,
        Assembly sourceAssembly)
    {
        var types = sourceAssembly
            .GetTypes()
            .Where(t => t.IsClass && t.IsAssignableTo(typeof(IXaelithService)));

        foreach (var type in types)
        {
            var ifaces = type.GetInterfaces().Where(
                t => t != typeof(IXaelithService) 
                       && t.IsAssignableTo(typeof(IXaelithService))
            );

            foreach (var iface in ifaces)
            {
                if (iface.IsAssignableTo(typeof(IXaelithTransientService)))
                {
                    services.AddTransient(
                        iface,
                        type
                    );
                }
                else
                {
                    services.AddSingleton(
                        iface,
                        type
                    );
                }
            }
        }

        return services;
    }
}