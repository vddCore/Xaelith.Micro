namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Xaelith.Micro.Infrastructure.DataModel.Shared;

public interface IToastService : IXaelithService
{
    event Action<ToastData> OnDisplayed;
    
    void Show(
        string message,
        ToastSeverity severity,
        int delayMilliseconds = 2000
    );
    
    Task DisplayedAsync();
}