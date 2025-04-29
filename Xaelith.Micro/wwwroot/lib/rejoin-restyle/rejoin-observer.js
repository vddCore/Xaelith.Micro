/**
 * https://github.com/dotnet/aspnetcore/issues/57453#issuecomment-2715608750
 *
 * can't wait for frickin' .NET 10 already.
 *
 * ~V */

const observer = new MutationObserver(() => {
let dialog = document.getElementById('components-reconnect-modal');

if (dialog && dialog.shadowRoot) {
    let shadowRoot = dialog.shadowRoot;
    let computedStyle = window.getComputedStyle(document.body);

    let dots = shadowRoot.querySelectorAll('.components-rejoining-animation div');
        dots.forEach(dot => {
            dot.style.borderColor = computedStyle
              .getPropertyValue('--components-reconnect-foreground');
        });
    
    let reconnectDialog = shadowRoot.querySelector('.components-reconnect-dialog');
    if (reconnectDialog) {
        reconnectDialog.style.backgroundColor = computedStyle
          .getPropertyValue('--components-reconnect-background');
          
        reconnectDialog.style.boxShadow = "0px 0px 5px " + computedStyle
          .getPropertyValue('--components-reconnect-shadow');
          
        reconnectDialog.style.borderColor = computedStyle
          .getPropertyValue('--components-reconnect-border');
          
        reconnectDialog.style.borderWidth = "1px";
        reconnectDialog.style.borderStyle = "solid";
        
        reconnectDialog.style.maxWidth = "20rem";
        reconnectDialog.style.width = "72%";
    }

    let retryButton = shadowRoot.querySelector("button");
    if (retryButton) {
        retryButton.style.backgroundColor = computedStyle
          .getPropertyValue("--button-background");
          
        retryButton.style.color = computedStyle
          .getPropertyValue("--button-foreground");
        
        retryButton.style.padding = "4px 20px";
        retryButton.style.borderRadius = "6px";
        retryButton.style.cursor = "pointer";
        retryButton.style.transition = "background-color 0.3s ease-in-out";
        retryButton.style.fontFamily = "Rajdhani";
        retryButton.style.textTransform = "uppercase"

        retryButton.addEventListener("mouseenter", () => {
            retryButton.style.backgroundColor = computedStyle
              .getPropertyValue("--button-hover-background");
        });
        
        retryButton.addEventListener("mouseleave", () => {
            retryButton.style.backgroundColor = computedStyle
              .gettPropertyValue("--button-background");
        });
    }
}
});

observer.observe(document.body, { childList: true, subtree: true });
