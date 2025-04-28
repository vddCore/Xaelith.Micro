namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

public class DuplicateNavListException : Exception
{
    public string Name { get; }

    public DuplicateNavListException(string message, string name)
        : base(message)
    {
        Name = name;
    }
}