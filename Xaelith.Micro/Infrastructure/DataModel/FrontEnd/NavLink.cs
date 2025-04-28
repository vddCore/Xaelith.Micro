namespace Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

using Newtonsoft.Json;

public record NavLink
{
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;
    
    [JsonProperty("href")]
    public string Target { get; set; } = string.Empty;
}