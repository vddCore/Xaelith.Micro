namespace Xaelith.Micro.Infrastructure.DataModel.Core;

public class ConfigurationEventArgs(Configuration config)
{
    public Configuration Config { get; } = config;
}