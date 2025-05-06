namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Newtonsoft.Json;

public class Statistics
{
    [JsonProperty]
    public Dictionary<Guid, long> PostViews { get; set; } = new();
    
    [JsonProperty("drafts")]
    public int Drafts { get; set; }
    
    [JsonProperty("blog_posts")]
    public int BlogPosts { get; set; }
    
    [JsonProperty("static_pages")]
    public int StaticPages { get; set; }

    [JsonProperty("disk_usage")]
    public long DiskUsage { get; set; }
    
    [JsonProperty("dashboard_view_count")]
    public int DashboardViewCount { get; set; }
}