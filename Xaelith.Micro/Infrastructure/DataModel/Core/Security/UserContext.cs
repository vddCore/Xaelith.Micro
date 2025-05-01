namespace Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public class UserContext
{
    public User? User { get; set; }
    public bool IsAuthenticated { get; set; }
}