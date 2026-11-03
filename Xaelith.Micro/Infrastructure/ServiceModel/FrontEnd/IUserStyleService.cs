namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

public interface IUserStyleService : IXaelithService
{
    string UserStyleFilePath { get; }
    
    string Fetch();
    void Update(string content);
}