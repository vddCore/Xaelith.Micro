namespace Xaelith.Micro.Infrastructure.DataModel.Content;

using Newtonsoft.Json;

public record Post
{
    [JsonProperty("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [JsonProperty("slug")]
    public string Slug { get; set; }
    
    [JsonProperty("published")]
    public bool Published { get; set; }
    
    [JsonProperty("publish_date")]
    public DateTime? PublishDate { get; set; }
    
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

    [JsonProperty("break_at")]
    public int? BreakAt { get; set; }

    public Post()
    {
        Slug = Id.ToString("D");
    }
}