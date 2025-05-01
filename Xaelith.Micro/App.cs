namespace Xaelith.Micro;

using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Xaelith.Micro.Infrastructure.ServiceModel.Core;
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
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x =>
            {
                x.LoginPath = "/admin/login";
                x.LogoutPath = "/admin/logout";
                x.AccessDeniedPath = "/admin/denied";
            });
        
        builder.Services.AddAuthorization();
        builder.Services.AddCascadingAuthenticationState();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}