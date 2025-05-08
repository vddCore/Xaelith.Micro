namespace Xaelith.Micro.Infrastructure.DataModel.Shared;

public record MediaModalData(
    Guid PostId,
    Action<bool> OnClose
) : ModalData(OnClose);
