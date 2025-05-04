namespace Xaelith.Micro.Infrastructure.DataModel.Shared;

public record DialogModalData(
    string Title,
    string Message,
    string ConfirmLabel,
    string CancelLabel,
    DialogModalSeverity Severity,
    Action<bool> OnClosed
) : ModalData(OnClosed);