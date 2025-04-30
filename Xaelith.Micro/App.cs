namespace Xaelith.Micro;

using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Xaelith.Micro.Infrastructure.DataModel.Core.Authentication;
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
        
        builder.Services.AddScoped(
            typeof(IPasswordHasher<>),
            typeof(BCryptPasswordHasher<>)
        );

        builder.Services.AddIdentity<User, IdentityRole>()
            .AddUserStore<FlatFileUserStore>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication()
            .AddCookie(options =>
            {
                options.LoginPath = "/admin/login";
                options.LogoutPath = "/admin/logout";
                options.AccessDeniedPath = "/admin/denied";
                options.Cookie.Name = "Xaelith.Micro__AuthCookie";
            });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}