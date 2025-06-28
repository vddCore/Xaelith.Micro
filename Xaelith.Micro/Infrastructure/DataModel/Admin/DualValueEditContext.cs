namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public class DualValueEditContext
{
    public record Entry
    {
        public string FirstValue { get; set; }
        public string SecondValue { get; set; }
        public object? UserData { get; set; }

        public Entry(string firstValue, string secondValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
        }
    }

    public List<Entry> Entries { get; set; } = [];

    public DualValueEditContext Copy()
    {
        return new DualValueEditContext
        {
            Entries = Entries.Select(x => x with { }).ToList(),
        };
    }
}