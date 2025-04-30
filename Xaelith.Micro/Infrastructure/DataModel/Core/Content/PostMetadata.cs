namespace Xaelith.Micro.Infrastructure.DataModel.Core.Content;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public record PostMetadata
{
    [JsonProperty("slug")]
    public string Slug { get; set; } = "untitled";
    
    [JsonProperty("published")]
    public bool Published { get; set; }

    [JsonProperty("publish_date")]
    public required DateTime PublishDate { get; set; } = DateTime.Now;
    
    [JsonProperty("edit_date")]
    public DateTime? EditDate { get; set; }
    
    [JsonProperty("author")]
    public required string Author { get; set; } = "nobody";
    
    [JsonProperty("title")]
    public required string Title { get; set; } = "untitled";
    
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("category")]
    public required string Category { get; set; } = string.Empty;
    
    [JsonProperty("tags")]
    public List<string> Tags { get; set; } = [];
    
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PostType Type { get; set; }
}