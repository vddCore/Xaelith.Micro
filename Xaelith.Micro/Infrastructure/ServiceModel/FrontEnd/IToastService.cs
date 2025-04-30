namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public interface IToastService : IXaelithService
{
    event Action<ToastData> OnDisplayed;
    
    void Show(string message, ToastSeverity severity);
    Task DisplayedAsync();
}