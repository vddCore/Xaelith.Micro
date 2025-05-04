namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Microsoft.AspNetCore.Components;
using Xaelith.Micro.Infrastructure.DataModel.Shared;

public class ModalService : IModalService
{
    private bool _isDisplayingModal;
    private readonly Queue<ModalData> _modalQueue = new();

    private List<Func<ModalData, Task>> _displayedCallbacks = new();

    public async Task ShowAsync(
        Action<bool> onClose,
        string title,
        string message,
        string confirmLabel,
        string cancelLabel,
        ModalSeverity severity)
    {
        _modalQueue.Enqueue(
            new ModalData(
                onClose,
                title,
                message,
                confirmLabel,
                cancelLabel,
                severity
            )
        );
        
        await TryShowNext();
    }

    private async Task TryShowNext()
    {
        if (_isDisplayingModal || _modalQueue.Count == 0)
            return;
        
        _isDisplayingModal = true;
        
        var modal = _modalQueue.Dequeue();

        var tasks = new List<Task>();
        foreach (var callback in _displayedCallbacks)
            tasks.Add(callback(modal));
        
        await Task.WhenAll(tasks);
    }

    public async Task DisplayedAsync()
    {
        _isDisplayingModal = false;
        await TryShowNext();
    }

    public void RegisterDisplayedCallback(Func<ModalData, Task> callback)
    {
        if (!_displayedCallbacks.Contains(callback))
            _displayedCallbacks.Add(callback);
    }

    public void UnregisterDisplayedCallback(Func<ModalData, Task> callback)
        => _displayedCallbacks.Remove(callback);
}