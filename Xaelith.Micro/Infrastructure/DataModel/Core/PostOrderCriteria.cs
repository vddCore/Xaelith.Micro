namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Newtonsoft.Json;

public enum PostOrderCriteria
{
    [JsonProperty("date")]
    Date,
    
    [JsonProperty("alphabetical")]
    Alphabetical
}