namespace Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public class NavList
{
    public string Name { get; set; } = string.Empty;
    public List<NavLink> Links { get; set; } = [];
}