namespace Xaelith.Micro.Infrastructure.Utilities;

using Xaelith.Micro.Infrastructure.ServiceModel;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AttachServices(
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSingleton<ConfigService>();

        return builder;
    }
}