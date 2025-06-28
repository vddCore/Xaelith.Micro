namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Newtonsoft.Json;

public record RegexPattern
{
    [JsonProperty("match")]
    public string Match { get; set; } = string.Empty;
    
    [JsonProperty("replace")]
    public string Replace { get; set; } = string.Empty;
    
    [JsonProperty("single_line")]
    public bool SingleLine { get; set; }
}