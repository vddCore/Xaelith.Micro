namespace Xaelith.Micro.Infrastructure.ServiceModel.Admin.Editor;

using System.Timers;
using Xaelith.Micro.Infrastructure.DataModel.Admin.Editor;
using Xaelith.Micro.Infrastructure.DataModel.Shared;
using Xaelith.Micro.Infrastructure.ServiceModel.Core;
using Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;
using Xaelith.Micro.Infrastructure.ServiceModel.Shared;
using Timer = System.Timers.Timer;

public class AutosaveService : IAutosaveService
{
    private readonly IConfigService _configService;
    private readonly IContentService _contentService;
    private readonly IToastService _toastService;

    private Timer? _autosaveTimer;

    private Func<EditorContext>? _provideState;

    public bool IsEnabled => _autosaveTimer?.Enabled ?? false;

    public AutosaveService(
        IConfigService configService,
        IContentService contentService,
        IToastService toastService)
    {
        _configService = configService;
        _contentService = contentService;
        _toastService = toastService;

        _autosaveTimer = new Timer(
            configService.Root!.Core.AutosaveIntervalSeconds * 1000
        );

        _autosaveTimer.Elapsed += AutosaveTimer_Elapsed;
    }

    public void Reset()
    {
        if (_autosaveTimer != null)
        {
            _autosaveTimer.Elapsed -= AutosaveTimer_Elapsed;
            _autosaveTimer?.Dispose();
        }

        _autosaveTimer = new Timer(
            _configService.Root!.Core.AutosaveIntervalSeconds * 1000
        );

        _autosaveTimer.Elapsed += AutosaveTimer_Elapsed;
    }

    public void Start(Func<EditorContext> provideState)
    {
        if (_autosaveTimer != null)
        {
            _provideState = provideState;
            _autosaveTimer.Start();
        }
    }

    public void Stop()
    {
        if (_autosaveTimer != null)
            _autosaveTimer.Stop();
    }

    public async Task SaveStateAsync(EditorContext context)
    {
        try
        {
            await _contentService.SavePostAsync(context);

            _toastService.Show(
                "Autosave success",
                ToastSeverity.Success
            );
        }
        catch (Exception ex)
        {
            _toastService.Show(
                $"Autosave failed: {ex.Message}",
                ToastSeverity.Error
            );
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
        if (_autosaveTimer != null)
        {
            _autosaveTimer.Elapsed -= AutosaveTimer_Elapsed;
            _autosaveTimer.Stop();
            _autosaveTimer.Dispose();
        }
    }
}