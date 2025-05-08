namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Microsoft.AspNetCore.Components;
using Xaelith.Micro.Infrastructure.DataModel.Shared;
using Xaelith.Micro.Visual.Components.Shared.Controls.Modals;

public class ModalService : IModalService
{
    private bool _isDisplayingModal;
    
    private readonly Dictionary<Type, Type> _dataComponentMap = new();
    private readonly Queue<ModalData> _modalQueue = new();
    
    private Func<ModalData, Type, Task>? _displayedCallback;

    public ModalService()
    {
        MapComponent<DialogModalData, DialogModal>();
        MapComponent<MediaModalData, MediaModal>();
    }
    
    public async Task ShowAsync<T>(T modalData)
        where T : ModalData
    {
        _modalQueue.Enqueue(modalData);
        
        await TryShowNext();
    }

    public void MapComponent<TData, TComponent>() 
        where TData : ModalData 
        where TComponent : ComponentBase
    {
        _dataComponentMap[typeof(TData)] = typeof(TComponent);
    }

    private async Task TryShowNext()
    {
        if (_isDisplayingModal || _modalQueue.Count == 0)
            return;
        
        var modalData = _modalQueue.Dequeue();
        var modalDataType = modalData.GetType();
        if (!_dataComponentMap.TryGetValue(modalDataType, out var componentType))
            return;

        if (_displayedCallback == null)
            return;
        
        _isDisplayingModal = true;
        await _displayedCallback(modalData, componentType);
    }

    public async Task DisplayedAsync()
    {
        _isDisplayingModal = false;
        await TryShowNext();
    }

    public void RegisterDisplayedCallback(Func<ModalData, Type, Task> callback)
        => _displayedCallback = callback;

    public void UnregisterDisplayedCallback()
        => _displayedCallback = null;
}