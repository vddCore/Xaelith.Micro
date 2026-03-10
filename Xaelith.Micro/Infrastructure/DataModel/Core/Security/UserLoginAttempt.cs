namespace Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public record UserLoginAttempt
{
    public int FailedAttempts { get; set; }
    public DateTime FirstAttemptTime { get; set; }
    public DateTime LastAttemptTime { get; set; }
    public DateTime? LockoutUntil { get; set; }
}