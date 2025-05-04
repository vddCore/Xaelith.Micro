namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public class CategoryEditContext
{
    public class Category
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    
    public List<Category> Categories { get; set; } = [];
}