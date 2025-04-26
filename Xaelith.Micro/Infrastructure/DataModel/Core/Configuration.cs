namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public class Configuration
{
    public GeneralSettings General { get; } = new();
}

public record GeneralSettings
{
    public string SiteTitle { get; set; } = "Xaelith Blog";
    public List<NavList> Links { get; set; } = [];
}