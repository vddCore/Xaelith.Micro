namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using Microsoft.AspNetCore.Identity;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public interface IFlatFileUserStore : IXaelithService
{
    List<User> GetAllUsers();
    Task<string> GetUserIdAsync(User user);
    Task<string?> GetUserNameAsync(User user);
    Task SetUserNameAsync(User user, string? newUserName);
    Task<IdentityResult> CreateAsync(User user);
    Task<IdentityResult> ModifyAsync(User user, Action<User> modify);
    Task<IdentityResult> DeleteAsync(User user);
    Task<User?> FindByIdAsync(string? userId);
    Task<User?> FindByIdAsync(Guid userId);
    Task<User?> FindByNameAsync(string? userName);
    Task SetPasswordHashAsync(User user, string? passwordHash);
    Task<string?> GetPasswordHashAsync(User user);
}