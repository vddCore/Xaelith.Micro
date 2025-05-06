namespace Xaelith.Micro.Visual.Pages.Base;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
    [Inject] protected IStatisticsService Statistics { get; private set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    
    protected virtual string Title => string.Empty;

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