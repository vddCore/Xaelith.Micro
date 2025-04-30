namespace Xaelith.Micro.Visual.Pages.Base;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Xaelith.Micro.Infrastructure.ServiceModel.Core;
using Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

public partial class XaelithPage : ComponentBase
{
    [Inject] protected IJSRuntime JS { get; private set; } = null!;
    [Inject] protected IConfigService Configuration { get; private set; } = null!;
    [Inject] protected IContentService Content { get; private set; } = null!;
    [Inject] protected IMarkdownService Markdown { get; private set; } = null!;

    protected virtual string Title => string.Empty;

    protected virtual async Task UpdateTitleAsync()
    {
        var title = $"{Configuration.Root!.General.SiteTitle}";

        if (!string.IsNullOrWhiteSpace(Title))
        {
            title += " // " + Title;
        }

        await JS.InvokeVoidAsync(
            "window.xaelith.setDocumentTitle",
            title
        );
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await UpdateTitleAsync();
        }
    }
}