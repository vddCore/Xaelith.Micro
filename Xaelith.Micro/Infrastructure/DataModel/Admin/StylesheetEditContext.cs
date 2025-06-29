namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public record StylesheetEditContext
{
    public string Code { get; set; } = string.Empty;
    public int Characters => Code.Length;
}