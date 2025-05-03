namespace Xaelith.Micro.Infrastructure.ServiceModel.Admin.Editor;

using Xaelith.Micro.Infrastructure.DataModel.Admin;

public interface IAutosaveService : IXaelithTransientService, IDisposable
{
    bool IsEnabled { get; }
    
    void Start(Func<EditorContext> provideState);
    void Stop();
    
    Task SaveStateAsync(EditorContext context);
}