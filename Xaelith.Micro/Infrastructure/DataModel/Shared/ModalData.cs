namespace Xaelith.Micro.Infrastructure.DataModel.Shared;

using Microsoft.AspNetCore.Components;

public record ModalData(
    EventCallback<bool> OnClosed,
    string Title,
    string Message,
    string ConfirmLabel,
    string CancelLabel,
    ModalSeverity Severity
);
