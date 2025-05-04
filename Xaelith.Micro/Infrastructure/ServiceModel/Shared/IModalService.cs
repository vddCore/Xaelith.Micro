namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Xaelith.Micro.Infrastructure.DataModel.Shared;

public interface IModalService : IXaelithService
{
    Task ShowAsync(
        Action<bool> onClose,
        string title,
        string message,
        string confirmLabel,
        string cancelLabel,
        ModalSeverity severity
    );

    Task DisplayedAsync();
    
    void RegisterDisplayedCallback(Func<ModalData, Task> callback);
    void UnregisterDisplayedCallback(Func<ModalData, Task> callback);

}