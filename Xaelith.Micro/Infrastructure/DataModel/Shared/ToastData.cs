namespace Xaelith.Micro.Infrastructure.DataModel.Shared;

public record ToastData(
    string Message,
    ToastSeverity Severity,
    int DelayMilliseconds = 2000
);