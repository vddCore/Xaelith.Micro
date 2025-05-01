namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using Microsoft.AspNetCore.Identity;

public interface IBCryptPasswordHasher : IXaelithService
{
    string Hash(string password);
    PasswordVerificationResult Verify(string plaintext, string hash);
}