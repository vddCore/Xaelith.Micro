namespace Xaelith.Micro.Infrastructure.ServiceModel.Core;

using Xaelith.Micro.Infrastructure.DataModel.Core;

public interface IConfigService : IXaelithService
{
    Configuration? Config { get; }
    
    void Reload();
    void Save();
}