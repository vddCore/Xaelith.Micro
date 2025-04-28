namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public interface INavigationService : IXaelithService
{
    NavList? FindNavList(string name);
    NavList CreateNavList(string name);
    bool RemoveNavList(string name);
    NavLink AddLink(NavList navList, string label, string target);
    bool RemoveLink(NavList navList, string label);
}