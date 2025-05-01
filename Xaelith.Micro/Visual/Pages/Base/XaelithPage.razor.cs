namespace Xaelith.Micro.Visual.Pages.Base;

using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;
using Xaelith.Micro.Infrastructure.ServiceModel.Core;
using Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;
using Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

public partial class XaelithPage : ComponentBase
{   
    [Inject] protected IJSRuntime JS { get; private set; } = null!;
    [Inject] protected IConfigService Configuration { get; private set; } = null!;
    [Inject] protected IContentService Content { get; private set; } = null!;
    [Inject] protected IMarkdownService Markdown { get; private set; } = null!;
    [Inject] protected IFlatFileUserStore UserStore { get; private set; } = null!;

    [CascadingParameter]
    protected Task<AuthenticationState>? AuthenticationState { get; set; }
    
    protected User? User { get; private set; }
    protected bool IsUserAuthenticated { get; private set; }

    protected virtual string Title => string.Empty;

    protected override async Task OnInitializedAsync()
    {
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await UpdateTitleAsync();
        }
    }
    
    protected virtual async Task UpdateTitleAsync()
    {
        var baseTitle = $"{Configuration.Root!.General.SiteTitle}";

        if (!string.IsNullOrWhiteSpace(Title))
        {
            baseTitle += " // " + Title;
        }

        await JS.InvokeVoidAsync(
            "window.xaelith.setDocumentTitle",
            baseTitle
        );
    }
}