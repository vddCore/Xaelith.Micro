namespace Xaelith.Micro.Infrastructure.Utilities;

using Xaelith.Micro.Infrastructure.ServiceModel.Core;
using Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection AttachXaelithServices(
        this IServiceCollection services)
    {
        services
            .AddSingleton<ConfigService>()
            .AddSingleton<NavigationService>();

        return services;
    }
}