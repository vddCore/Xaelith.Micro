namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public enum PostOrderDirection
{
    [JsonProperty("ascending")]
    [Display(Name = "Ascending")]
    Ascending,
    
    [JsonProperty("descending")]
    [Display(Name = "Descending")]
    Descending
}