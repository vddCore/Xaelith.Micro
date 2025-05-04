namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Microsoft.AspNetCore.Components;
using Xaelith.Micro.Infrastructure.DataModel.Shared;

public interface IModalService : IXaelithService
{
    public event Action<ModalData>? OnDisplayed;

    Task ShowAsync(
        EventCallback<bool> onClose,
        string title,
        string message,
        string confirmLabel,
        string cancelLabel,
        ModalSeverity severity
    );

    void Displayed();

}