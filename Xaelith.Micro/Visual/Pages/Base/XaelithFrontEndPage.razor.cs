namespace Xaelith.Micro.Visual.Pages.Base;

using Microsoft.JSInterop;

public partial class XaelithFrontEndPage : XaelithPage
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await JS.InvokeVoidAsync("xaelith.initMobileView");
        }
    }
}