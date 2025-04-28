namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public class DuplicateLinkException : Exception
{
    public string Label { get; }
    public NavList NavigationList { get; }

    public DuplicateLinkException(
        string message,
        string label,
        NavList navigationList)
        : base(message)
    {
        Label = label;
        NavigationList = navigationList;
    }
}