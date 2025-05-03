namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public class ToastService : IToastService
{
    public event Action<ToastData>? OnDisplayed;

    private bool _isDisplayingToast;
    private readonly Queue<ToastData> _toastQueue = new();
    
    public void Show(string message, ToastSeverity severity)
    {
        _toastQueue.Enqueue(new(message, severity));
        TryShowNext();
    }

    private void TryShowNext()
    {
        if (_isDisplayingToast || _toastQueue.Count == 0)
            return;
        
        _isDisplayingToast = true;
        
        var toast = _toastQueue.Dequeue();
        OnDisplayed?.Invoke(toast);
    }

    public async Task DisplayedAsync()
    {
        await Task.Delay(2000);
        _isDisplayingToast = false;
        TryShowNext();
    }
}