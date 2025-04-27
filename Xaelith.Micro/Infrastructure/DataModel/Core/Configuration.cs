namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public class Configuration
{
    public GeneralSettings General { get; } = new();
}

public record GeneralSettings
{
    public string SiteTitle { get; set; } = "Xaelith Blog";
    public string SiteDescription { get; set; } = "A blog powered by Xaelith.";
    public string SiteUrl { get; set; } = "https://localhost";
    public List<NavList> Links { get; set; } = [];
}