namespace Xaelith.Micro.Infrastructure.DataModel.Core.Content;

using Newtonsoft.Json;

public record Category
{
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("show_on_main_page")]
    public bool ShowOnMainPage { get; set; } = true;
}