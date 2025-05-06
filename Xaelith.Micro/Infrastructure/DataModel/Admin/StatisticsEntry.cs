namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

public class StatisticsEntry
{
    public string Name { get; }
    public Func<object?> ValueExpression { get; }

    public StatisticsEntry(string name, Func<object?> valueExpression)
    {
        Name = name;
        ValueExpression = valueExpression;
    }
}