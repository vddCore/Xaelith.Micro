namespace Xaelith.Micro.Infrastructure.ServiceModel.Shared;

using Microsoft.AspNetCore.Components;
using Xaelith.Micro.Infrastructure.DataModel.Shared;

public interface IModalService : IXaelithScopedService
{
    Task ShowAsync<T>(T modalData)
        where T : ModalData;

    void MapComponent<TData, TComponent>()
        where TData : ModalData
        where TComponent : ComponentBase;

    void RegisterDisplayedCallback(Func<ModalData, Type, Task> callback);
    void UnregisterDisplayedCallback();

    Task DisplayedAsync();
}