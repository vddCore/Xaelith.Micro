namespace Xaelith.Micro.Infrastructure.ServiceModel.Admin.Editor;

using System.Timers;
using Xaelith.Micro.Infrastructure.DataModel.Admin;
using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;
using Xaelith.Micro.Infrastructure.ServiceModel.Core;
using Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;
using Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;
using Timer = System.Timers.Timer;

public class AutosaveService : IAutosaveService
{
    private readonly IContentService _contentService;
    private readonly IToastService _toastService;
    private readonly Timer _autosaveTimer;

    private Func<EditorContext>? _provideState;
    
    public bool IsEnabled => _autosaveTimer.Enabled;

    public AutosaveService(
        IConfigService configService,
        IContentService contentService,
        IToastService toastService)
    {
        _contentService = contentService;
        _toastService = toastService;
        
        _autosaveTimer = new Timer(
            configService.Root!.Core.AutosaveIntervalSeconds * 1000
        );
        
        _autosaveTimer.Elapsed += AutosaveTimer_Elapsed;
    }

    public void Start(Func<EditorContext> provideState)
    {
        _provideState = provideState;
        _autosaveTimer.Start();
    }

    public void Stop()
        => _autosaveTimer.Stop();

    public async Task SaveStateAsync(EditorContext context)
    {
        try
        {
            await _contentService.SavePostAsync(context);
            _toastService.Show("Autosave success", ToastSeverity.Success);
        }
        catch (Exception ex)
        {
            _toastService.Show($"Autosave failed: {ex.Message}", ToastSeverity.Error);
        }
    }

    private async void AutosaveTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        var context = _provideState?.Invoke();

        if (context != null)
            await SaveStateAsync(context);
    }
    
    public void Dispose()
    {
        _autosaveTimer.Stop();
        _autosaveTimer.Dispose();
    }
}