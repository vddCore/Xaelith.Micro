namespace Xaelith.Micro.Infrastructure.Utilities;

public static class WellKnown
{
    public static string Root { get; } = AppContext.BaseDirectory;
    public static string Data { get; } = Path.Combine(Root, "data");
    
    public static string Logs { get; } = Path.Combine(Data, "logs");
    
    public static string Config { get; } = Path.Combine(Data, "config");
    public static string Content { get; } = Path.Combine(Data, "content");
    
    public static string UserStore { get; } = Path.Combine(Config, "users");

    public static string ConfigurationFileName { get; } = "xaelith.json";
    public static string StatisticsFileName { get; } = "stats.json";

    public static string PostMediaDirectoryName { get; } = "media";
    public static string PostMetadataFileName { get; } = "meta.json";
    public static string PostBodyFileName { get; } = "body.md";
    public static string UserCssFileName { get; } = "user.css";
}