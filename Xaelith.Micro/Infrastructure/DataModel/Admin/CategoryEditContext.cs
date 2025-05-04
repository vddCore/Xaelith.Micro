namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public class CategoryEditContext
{
    public record Category(string name, string description)
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
    }

    public List<Category> Categories { get; set; } = [];
}