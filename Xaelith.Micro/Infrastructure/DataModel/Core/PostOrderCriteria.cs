namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public enum PostOrderCriteria
{
    [Display(Name = "Date Published")]
    [JsonProperty("date")]
    Date,
    
    [Display(Name = "Alphabetical")]
    [JsonProperty("alphabetical")]
    Alphabetical
}