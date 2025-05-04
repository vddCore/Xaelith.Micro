namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Xaelith.Micro.Infrastructure.DataModel.Shared;

public class ToastService : IToastService
{
    public event Action<ToastData>? OnDisplayed;

    private bool _isDisplayingToast;
    private ToastData? _toastData;
    
    private readonly Queue<ToastData> _toastQueue = new();
    
    public void Show(
        string message,
        ToastSeverity severity,
        int delayMilliseconds = 2000)
    {
        _toastQueue.Enqueue(new(message, severity));
        TryShowNext();
    }

    private void TryShowNext()
    {
        if (_isDisplayingToast || _toastQueue.Count == 0)
            return;
        
        _isDisplayingToast = true;
        
        _toastData = _toastQueue.Dequeue();
        OnDisplayed?.Invoke(_toastData);
    }

    public async Task DisplayedAsync()
    {
        await Task.Delay(_toastData?.DelayMilliseconds ?? 2000);
        _isDisplayingToast = false;
        TryShowNext();
    }
}