namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Newtonsoft.Json;

public enum PostOrderDirection
{
    [JsonProperty("ascending")]
    Ascending,
    
    [JsonProperty("descending")]
    Descending
}