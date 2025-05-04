namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public class DualValueEditContext
{
    public record Entry(string firstValue, string secondValue)
    {
        public string FirstValue { get; set; } = firstValue;
        public string SecondValue { get; set; } = secondValue;
    }

    public List<Entry> Entries { get; set; } = [];
}