namespace Xaelith.Micro.Infrastructure.ServiceModel.Core;

using Xaelith.Micro.Infrastructure.DataModel.Core;

public interface IConfigService : IXaelithService
{
    event EventHandler<ConfigurationEventArgs>? Modified;
    
    Configuration? Root { get; }
 
    void Reload();
    void Save();
}