namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

public interface ILoginAttemptTracker : IXaelithService
{
    void RecordFailedAttempt(string username, string ipAddress);
    void ClearFailedAttempts(string username);
    bool IsAccountLocked(string username);
    bool IsIpRateLimited(string ipAddress);
    int GetRemainingLockoutSeconds(string username);
}
