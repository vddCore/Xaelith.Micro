namespace Xaelith.Micro.Infrastructure.DataModel.Core.Security;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public record User
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
    [JsonProperty("login_name")]
    public string LoginName { get; set; } = string.Empty;

    [JsonProperty("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonProperty("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UnixEpoch;
    
    [JsonProperty("is_enabled")]
    public bool IsEnabled { get; set; }
    
    [JsonProperty("user_role")]
    [JsonConverter(typeof(StringEnumConverter))]
    public UserRole UserRole { get; set; }
}