namespace Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public record IpLoginAttempt
{
    public int AttemptCount { get; set; }
    public DateTime WindowStart { get; set; }
}