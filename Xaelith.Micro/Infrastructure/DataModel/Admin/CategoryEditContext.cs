namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public class CategoryEditContext
{
    public record Entry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShowOnMainPage { get; set; }
        
        public Entry(string name, string description, bool showOnMainPage)
        {
            Name = name;
            Description = description;
            ShowOnMainPage = showOnMainPage;
        }
    }

    public List<Entry> Entries { get; set; } = [];

    public CategoryEditContext Copy()
    {
        return new CategoryEditContext
        {
            Entries = Entries.Select(x => x with { }).ToList(),
        };
    }
}