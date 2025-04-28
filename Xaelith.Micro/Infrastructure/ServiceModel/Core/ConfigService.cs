namespace Xaelith.Micro.Infrastructure.ServiceModel.Core;

using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.Core;
using Xaelith.Micro.Infrastructure.Utilities;

public class ConfigService
{
    public string ConfigPath { get; } = Path.Combine(
        KnownPaths.Config,
        "xaelith.json"
    );
    
    public Configuration? Config { get; private set; }

    public ConfigService()
    {
        Reload();
    }
    
    public void Reload()
    {
        if (!File.Exists(ConfigPath))
        {
            Config = new Configuration();
            Save();
        }
        else
        {
            using var sr = new StreamReader(ConfigPath);
            
            Config = JsonConvert.DeserializeObject<Configuration>(
                sr.ReadToEnd()
            );

            if (Config == null)
            {
                Config = new Configuration();
                Save();
            }
        }
    }

    public void Save()
    {
        using (var sw = new StreamWriter(ConfigPath))
        {
            sw.Write(JsonConvert.SerializeObject(Config));
        }
    }
}