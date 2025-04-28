namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;
using Xaelith.Micro.Infrastructure.ServiceModel.Core;

public class NavigationService : INavigationService
{
    private readonly IConfigService _configService;
    
    public NavigationService(IConfigService configService)
    {
        _configService = configService;
    }

    public NavList? FindNavList(string name)
    {
        return _configService.Root?
            .Navigation
            .NavigationLists
            .FirstOrDefault(x => x.Name == name);
    }

    public NavList CreateNavList(string name)
    {
        if (FindNavList(name) != null)
        {
            throw new DuplicateNavListException(
                "A navigation list with the same name already exists.", 
                name
            );
        }

        var navList = new NavList { Name = name };
        
        _configService.Root?
            .Navigation
            .NavigationLists
            .Add(navList);
        
        _configService.Save();
        
        return navList;
    }

    public bool RemoveNavList(string name)
    {
        var navList = FindNavList(name);

        if (navList == null)
            return false;
        
        _configService.Root?
            .Navigation
            .NavigationLists
            .Remove(navList);
        
        _configService.Save();
        return true;
    }

    public NavLink AddLink(NavList navList, string label, string target)
    {
        if (navList.Links.Exists(x => x.Label == label))
        {
            throw new DuplicateLinkException(
                "A link with the same label already exists in the provided navigation list.",
                label,
                navList
            );
        }

        var navLink = new NavLink
        {
            Label = label,
            Target = target
        };
        
        navList.Links.Add(navLink);
        
        _configService.Save();
        
        return navLink;
    }

    public bool RemoveLink(NavList navList, string label)
    {
        var removed = navList.Links.RemoveAll(x => x.Label == label) > 0;

        if (removed)
        {
            _configService.Save();
        }
        
        return removed;
    }
}