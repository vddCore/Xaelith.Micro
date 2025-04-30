namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public class BCryptPasswordHasher<T> : IBCryptPasswordHasher<T>
{
    public string HashPassword(User user, string password)
        => BCrypt.HashPassword(password);

    public PasswordVerificationResult VerifyHashedPassword(
        User user,
        string hashedPassword,
        string providedPassword)
    {
        return BCrypt.Verify(providedPassword, hashedPassword)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }
}