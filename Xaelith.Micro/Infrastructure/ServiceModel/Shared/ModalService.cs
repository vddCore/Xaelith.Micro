namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Microsoft.AspNetCore.Components;
using Xaelith.Micro.Infrastructure.DataModel.Shared;

public class ModalService : IModalService
{
    public event Action<ModalData>? OnDisplayed;

    private bool _isDisplayingModal;
    private readonly Queue<ModalData> _modalQueue = new();
    
    public async Task ShowAsync(
        EventCallback<bool> onClose,
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
        
        TryShowNext();
    }

    private void TryShowNext()
    {
        if (_isDisplayingModal || _modalQueue.Count == 0)
            return;
        
        _isDisplayingModal = true;
        
        var modal = _modalQueue.Dequeue();
        OnDisplayed?.Invoke(modal);
    }

    public void Displayed()
    {
        _isDisplayingModal = false;
        TryShowNext();
    }
}