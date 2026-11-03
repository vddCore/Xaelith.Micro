namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.Core;
using Xaelith.Micro.Infrastructure.Utilities;

public class RegexPatternService : IRegexPatternService
{
    public string RegexPatternStoreFilePath { get; set; } = Path.Combine(
        WellKnown.Config,
        WellKnown.RegexPatternStoreFileName
    );

    public List<RegexPattern> Fetch()
    {
        if (!File.Exists(RegexPatternStoreFilePath))
        {
            using (var sw = new StreamWriter(RegexPatternStoreFilePath))
            {
                sw.WriteLine("[]");
            }
        }
        
        var patternStoreJson = File.ReadAllText(RegexPatternStoreFilePath);
        if (string.IsNullOrEmpty(patternStoreJson))
        {
            return [];
        }

        return JsonConvert.DeserializeObject<List<RegexPattern>>(
            patternStoreJson
        ) ?? [];
    }

    public void Update(List<RegexPattern> patterns)
    {
        using (var sw = new StreamWriter(RegexPatternStoreFilePath))
        {
            sw.WriteLine(JsonConvert.SerializeObject(patterns));
        }
    }
}