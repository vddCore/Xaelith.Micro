namespace Xaelith.Micro.Infrastructure.ServiceModel.Core;

using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.Core;
using Xaelith.Micro.Infrastructure.Utilities;

public class ConfigService : IConfigService
{
    public string ConfigPath { get; } = Path.Combine(
        WellKnown.Config,
        WellKnown.ConfigurationFileName
    );
    
    public event EventHandler<ConfigurationEventArgs>? Modified;
    
    public Configuration? Root { get; private set; }
    
    public ConfigService()
    {
        Reload();
    }
    
    public void Reload()
    {
        if (!File.Exists(ConfigPath))
        {
            Root = new Configuration();
            Save();
        }
        else
        {
            using var sr = new StreamReader(ConfigPath);
            
            Root = JsonConvert.DeserializeObject<Configuration>(
                sr.ReadToEnd()
            );

            if (Root == null)
            {
                Root = new Configuration();
                Save();
            }
        }
    }

    public void Save()
    {
        if (Root == null)
            return;
        
        using (var sw = new StreamWriter(ConfigPath))
        {
            sw.Write(JsonConvert.SerializeObject(Root));
        }
        
        Modified?.Invoke(this, new ConfigurationEventArgs(Root));
    }
}