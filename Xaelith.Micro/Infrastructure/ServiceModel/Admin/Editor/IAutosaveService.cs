namespace Xaelith.Micro.Infrastructure.ServiceModel.Admin.Editor;

using Xaelith.Micro.Infrastructure.DataModel.Admin.Editor;

public interface IAutosaveService : IXaelithTransientService, IDisposable
{
    bool IsEnabled { get; }

    void Reset();
    void Start(Func<EditorContext> provideState);
    void Stop();
    
    Task SaveStateAsync(EditorContext context);
}