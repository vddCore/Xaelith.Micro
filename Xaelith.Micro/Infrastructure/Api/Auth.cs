namespace Xaelith.Micro.Infrastructure.Api;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public static class Auth
{
    public static class Endpoints
    {
        public const string Login = "/api/auth/login";
    }
    
    [HttpPost(Endpoints.Login)]
    public static async Task<IResult> Login(
        HttpContext httpContext,    
        SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        var form = await httpContext.Request.ReadFormAsync();
        var username = form["username"];
        var password = form["password"];
        var rememberLogin = bool.TryParse(
            form["rememberLogin"], 
            out var r
        ) && r;

        var user = await userManager.FindByNameAsync(username!);

        if (user == null)
            return Results.BadRequest("Invalid credentials.");
        
        var loginResult = await signInManager.PasswordSignInAsync(
            user,
            password!,
            rememberLogin,
            false
        );

        if (!loginResult.Succeeded)
            return Results.BadRequest("Invalid credentials.");
        
        var identity = new ClaimsIdentity(
            )
          
        await httpContext.SignInAsync(
            IdentityConstants.ApplicationScheme,
            claimsPrincipal
        );
    
        return Results.Ok();

    }
}