namespace Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

using Newtonsoft.Json;

public class NavList
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("links")]
    public List<NavLink> Links { get; set; } = [];
}