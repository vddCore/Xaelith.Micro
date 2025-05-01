namespace Xaelith.Micro.Visual.Pages.Base;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

[Authorize]
public partial class XaelithRestrictedPage
{
    [CascadingParameter]
    protected Task<AuthenticationState>? AuthenticationState { get; set; }

    protected User? User { get; private set; }
    protected bool IsUserAuthenticated { get; private set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        if (AuthenticationState != null)
        {
            var state = await AuthenticationState;

            User = await UserStore.FindByIdAsync(
                state.User.FindFirst(
                    x => x.Type == ClaimTypes.Sid
                )?.Value
            );
            
            IsUserAuthenticated = state.User.Identity?.IsAuthenticated ?? false;
        }
    }
}