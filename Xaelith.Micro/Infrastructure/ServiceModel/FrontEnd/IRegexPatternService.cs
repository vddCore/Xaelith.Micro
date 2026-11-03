namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Xaelith.Micro.Infrastructure.DataModel.Core;

public interface IRegexPatternService : IXaelithService
{
    string RegexPatternStoreFilePath { get; }
    
    List<RegexPattern> Fetch();
    void Update(List<RegexPattern> patterns);
}