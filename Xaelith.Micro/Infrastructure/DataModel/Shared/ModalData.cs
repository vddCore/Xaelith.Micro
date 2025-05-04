namespace Xaelith.Micro.Infrastructure.DataModel.Shared;

public abstract record ModalData(Action<bool> OnClosed)
{
    public bool IsOpen { get; set; }

    public void Close(bool result)
    {
        OnClosed(result);
        IsOpen = false;
    }
}
