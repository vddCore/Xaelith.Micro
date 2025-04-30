namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

/**
 * Won't be picked up by autoregistration system.
 **/
public class NoOpLookupNormalizer : ILookupNormalizer
{
    [return: NotNullIfNotNull("name")]
    public string? NormalizeName(string? name)
        => name;

    [return: NotNullIfNotNull("email")]
    public string? NormalizeEmail(string? email)
        => email;
}