namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public class Configuration
{
    [JsonProperty("general")]
    public GeneralSettings General { get; } = new();
    
    [JsonProperty("rendering")]
    public RenderingSettings Rendering { get; } = new();
    
    [JsonProperty("navigation")]
    public NavigationSettings Navigation { get; } = new();

    [JsonProperty("content")]
    public ContentSettings Content { get; } = new();
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
    
    [JsonProperty("date_format")]
    public string DateFormat { get; set; } = "dd MMMM yyyy - HH:mm";
    
    [JsonProperty("post_order_criteria")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PostOrderCriteria PostOrderCriteria { get; set; } = PostOrderCriteria.Date;
    
    [JsonProperty("post_order_direction")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PostOrderDirection PostOrderDirection { get; set; } = PostOrderDirection.Descending;
}

public record RenderingSettings
{
    [JsonProperty("pagebreak_token")]
    public string PageBreakToken { get; set; } = "[[pagebreak]]";
}

public record NavigationSettings
{
    [JsonProperty("nav_lists")]
    public List<NavList> NavigationLists { get; set; } = [];
}

public record ContentSettings
{
    [JsonProperty("categories")]
    public Dictionary<string, string> Categories { get; set; } = new();
    
    [JsonProperty("tags")]
    public Dictionary<string, string> Tags { get; set; } = new();
}