namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using Microsoft.AspNetCore.Identity;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

/**
 * Won't be picked up by autoregistration system.
 **/
public interface IBCryptPasswordHasher<T> : IPasswordHasher<User>
{
}