namespace Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public record ToastData(
    string Message,
    ToastSeverity Severity
);