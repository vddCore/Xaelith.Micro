namespace Xaelith.Micro;

using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using Xaelith.Micro.Infrastructure.Utilities;

public partial class App
{
    public static void Main(string[] args)
    {
        RenderModeSelector.RegisterRenderModeOverrides();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AttachXaelithServices(Assembly.GetExecutingAssembly())
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x =>
            {
                x.Cookie.Name = "Xaelith__Auth";
                x.Cookie.IsEssential = true;

                x.LoginPath = "/admin/login";
                x.LogoutPath = "/admin/logout";
                x.AccessDeniedPath = "/admin/denied";

                x.ExpireTimeSpan = TimeSpan.FromHours(1);
                x.SlidingExpiration = true;
            });

        builder.Services.AddAuthorization();
        builder.Services.AddCascadingAuthenticationState();
        builder.WebHost.UseStaticWebAssets();
        
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseForwardedHeaders(new ForwardedHeadersOptions {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor 
                               | ForwardedHeaders.XForwardedProto
        });
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(
                    builder.Environment.ContentRootPath,
                    WellKnown.Content
                )
            )
        });

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}