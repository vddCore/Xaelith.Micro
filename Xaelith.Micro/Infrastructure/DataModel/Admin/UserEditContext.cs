namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public record UserEditContext
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string LoginName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    
    public bool IsEnabled { get; set; }

    public UserEditContext()
    {
    }
    
    public UserEditContext(User user)
    {
        Id = user.Id;
        LoginName = user.LoginName;
        DisplayName = user.DisplayName;
        
        IsEnabled = user.IsEnabled;
    }
}