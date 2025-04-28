namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public class Configuration
{
    [JsonProperty("general")]
    public GeneralSettings General { get; } = new();
    
    [JsonProperty("navigation")]
    public NavigationSettings Navigation { get; } = new();
}

public record GeneralSettings
{
    [JsonProperty("site_title")]
    public string SiteTitle { get; set; } = "Xaelith Blog";
    
    [JsonProperty("site_description")]
    public string SiteDescription { get; set; } = "A blog powered by Xaelith";
    
    [JsonProperty("site_url")]
    public string SiteUrl { get; set; } = "https://localhost";
    
    [JsonProperty("footer_text")]
    public string FooterText { get; set; } = "Copyright (c) 2025 Xaelith Project";
}

public record NavigationSettings
{
    [JsonProperty("nav_lists")]
    public List<NavList> NavigationLists { get; set; } = [];
}