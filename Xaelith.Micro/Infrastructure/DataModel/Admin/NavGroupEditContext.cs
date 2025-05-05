namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public record NavGroupEditContext
{
    public required string Name { get; set; }
    public required DualValueEditContext Links { get; set; }
}