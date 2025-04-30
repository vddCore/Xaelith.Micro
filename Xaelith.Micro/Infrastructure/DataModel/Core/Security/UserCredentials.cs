namespace Xaelith.Micro.Infrastructure.DataModel.Core.Security;

using System.ComponentModel.DataAnnotations;

public class UserCredentials
{
    [Required] public string Username { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    
    public bool RememberLogin { get; set; }
}