namespace Xaelith.Micro.Infrastructure.DataModel.Shared;

public record ModalData(
    Action<bool> OnClosed,
    string Title,
    string Message,
    string ConfirmLabel,
    string CancelLabel,
    ModalSeverity Severity
);
