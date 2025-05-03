namespace Xaelith.Micro.Infrastructure.DataModel.Core.Content;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public record PostMetadata
{
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonProperty("published")]
    public bool Published { get; set; }

    [JsonProperty("publish_date")]
    public DateTime? PublishDate { get; set; }
    
    [JsonProperty("edit_date")]
    public DateTime? EditDate { get; set; }

    [JsonProperty("author")]
    public string Author { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;
    
    [JsonProperty("tags")]
    public List<string> Tags { get; set; } = [];
    
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PostType Type { get; set; }
}