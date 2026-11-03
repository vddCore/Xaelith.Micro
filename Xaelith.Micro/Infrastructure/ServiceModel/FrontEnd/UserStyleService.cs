namespace Xaelith.Micro.Infrastructure.ServiceModel.FrontEnd;

using Xaelith.Micro.Infrastructure.Utilities;

public class UserStyleService : IUserStyleService
{
    public string UserStyleFilePath { get; set; } = Path.Combine(
        WellKnown.Config, 
        WellKnown.UserCssFileName
    );

    public string Fetch()
    {
        if (!File.Exists(UserStyleFilePath))
        {
            File.Create(UserStyleFilePath).Dispose();
        }
        
        return File.ReadAllText(UserStyleFilePath);
    }

    public void Update(string content)
    {
        using (var sw = new StreamWriter(UserStyleFilePath))
        {
            sw.WriteLine(content);
        }
    }
}