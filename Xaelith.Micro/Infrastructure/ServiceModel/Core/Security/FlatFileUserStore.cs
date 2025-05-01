namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;
using Xaelith.Micro.Infrastructure.Utilities;

public class FlatFileUserStore : IFlatFileUserStore
{
    public FlatFileUserStore()
    {
        Directory.CreateDirectory(WellKnown.UserStore);
    }

    private List<User> LoadUsers()
    {
        var userFiles = Directory.GetFiles(WellKnown.UserStore, "*.json");
        var users = new List<User>();

        foreach (var file in userFiles)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(
                    File.ReadAllText(file)
                );

                if (user != null)
                {
                    users.Add(user);
                }
            }
            catch
            {
                /* Ignore, as if the user never existed. */
            }
        }

        return users;
    }

    private void SaveUser(User user)
    {
        var userPath = Path.Combine(WellKnown.UserStore, $"{user.Id:D}.json");
        File.WriteAllText(userPath, JsonConvert.SerializeObject(user));
    }

    public Task<string> GetUserIdAsync(User user)
        => Task.FromResult(user.Id.ToString("D"));

    public Task<string?> GetUserNameAsync(User user) 
        => Task.FromResult<string?>(user.LoginName);

    public Task SetUserNameAsync(User user, string? newUserName)
    {
        if (newUserName != null)
            user.LoginName = newUserName;
        
        SaveUser(user);

        return Task.CompletedTask;
    }

    public Task<IdentityResult> CreateAsync(User user)
    {
        var users = LoadUsers();
        
        foreach (var existingUser in users)
        {
            if (existingUser.Id == user.Id)
            {
                return Task.FromResult(
                    IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "UserIdAlreadyExists",
                            Description = "User with this ID already exists."
                        }
                    )
                );
            }

            if (existingUser.LoginName == user.LoginName)
            {
                return Task.FromResult(
                    IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "UserLogniNameAlreadyExists",
                            Description = "User with this login name already exists."
                        }
                    )
                );
            }
        }
        
        File.WriteAllText(
            Path.Combine(WellKnown.UserStore, $"{user.Id:D}.json"),
            JsonConvert.SerializeObject(user)
        );
        
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> UpdateAsync(User user)
    {
        var userPath = Path.Combine(WellKnown.UserStore, $"{user.Id:D}.json");

        if (!File.Exists(userPath))
        {
            return Task.FromResult(
                IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "UserIdNotFound",
                        Description = "User with this ID does not exist."
                    }
                )
            );
        }
        
        File.WriteAllText(
            userPath,
            JsonConvert.SerializeObject(user)
        );
        
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(User user)
    {
        var userPath = Path.Combine(WellKnown.UserStore, $"{user.Id:D}.json");

        if (!File.Exists(userPath))
        {
            return Task.FromResult(
                IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "UserIdNotFound",
                        Description = "User with this ID does not exist."
                    }
                )
            );
        }
        
        File.Delete(userPath);
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<User?> FindByIdAsync(string userId)
    {
        var userPath = Path.Combine(WellKnown.UserStore, $"{userId}.json");

        if (!File.Exists(userPath))
        {
            return Task.FromResult<User?>(null);
        }

        return Task.FromResult(
            JsonConvert.DeserializeObject<User>(
                File.ReadAllText(userPath)
            )
        );
    }

    public Task<User?> FindByNameAsync(string userName)
    {
        var users = LoadUsers();
        
        return Task.FromResult(
            users.FirstOrDefault(
                x => x.LoginName == userName
            )
        );
    }

    public Task SetPasswordHashAsync(User user, string? passwordHash)
    {
        if (passwordHash == null)
            return Task.CompletedTask;
        
        user.PasswordHash = passwordHash;
        SaveUser(user);
        
        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(User user)
        => Task.FromResult<string?>(user.PasswordHash);

    public Task<bool> HasPasswordAsync(User user) 
        => Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash)); 
}