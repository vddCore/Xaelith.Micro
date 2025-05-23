﻿namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

public class BCryptPasswordHasher : IBCryptPasswordHasher
{
    public string Hash(string password)
        => BCrypt.HashPassword(password);

    public PasswordVerificationResult Verify(string plaintext, string hash)
    {
        return BCrypt.Verify(plaintext, hash)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }
}