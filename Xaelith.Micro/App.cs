namespace Xaelith.Micro;

using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Xaelith.Micro.Infrastructure;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;
using Xaelith.Micro.Infrastructure.ServiceModel.Core;
using Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;
using Xaelith.Micro.Infrastructure.Utilities;

public partial class App
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AttachXaelithServices(Assembly.GetExecutingAssembly())
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services
            .AddHttpClient("http", (provider, client) =>
            {
                var configService = provider
                    .GetRequiredService<IConfigService>();

                var baseUrl = configService.Root?.Core.ApiUrl;

                if (string.IsNullOrEmpty(baseUrl))
                {
                    throw new InvalidOperationException(
                        "api_url not set in config."
                    );
                }

                client.BaseAddress = new Uri(baseUrl);
            });
        
        builder.Services.AddScoped(
            provider => provider
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("http")
        );

        builder.Services
            .AddSingleton<ILookupNormalizer, NoOpLookupNormalizer>();

        builder.Services.AddScoped(
            typeof(IPasswordHasher<User>),
            typeof(BCryptPasswordHasher<User>)
        );

        builder.Services.AddIdentityCore<User>()
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>()
            .AddUserStore<FlatFileUserStore>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.Cookie.Name = ".AspNetCore.Cookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.LoginPath = "/admin/login";
                options.LogoutPath = "/admin/logout";
                options.AccessDeniedPath = "/admin/denied";
            });

        var app = builder.Build();

        app.MapPost(
            Infrastructure.Api.Auth.Endpoints.Login,
            Infrastructure.Api.Auth.Login
        );

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseWhen(
            context => !context.Request.Path.StartsWithSegments("/api"),
            appBuilder => appBuilder.UseAntiforgery()
        );

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}