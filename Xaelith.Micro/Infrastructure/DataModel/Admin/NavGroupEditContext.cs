namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public record NavGroupEditContext
{
    public string Name { get; set; } = string.Empty;
    public DualValueEditContext Links { get; set; } = new();

    public NavGroupEditContext Copy()
    {
        return new NavGroupEditContext
        {
            Name = Name,
            Links = Links.Copy()
        };
    }
}