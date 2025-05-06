namespace Xaelith.Micro.Infrastructure.ServiceModel.Core;

using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.Core;
using Xaelith.Micro.Infrastructure.DataModel.Core.Content;
using Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;
using Xaelith.Micro.Infrastructure.Utilities;

public class StatisticsService : IStatisticsService
{
    private readonly IContentService _contentService;

    private string FilePath { get; }

    public Statistics Data { get; }

    public StatisticsService(IContentService contentService)
    {
        _contentService = contentService;

        FilePath = Path.Combine(
            WellKnown.Data,
            WellKnown.StatisticsFileName
        );

        try
        {
            var stats = JsonConvert.DeserializeObject<Statistics>(
                File.ReadAllText(FilePath)
            );

            Data = stats ?? new Statistics();
        }
        catch
        {
            Data = new Statistics();
            Save();
        }
    }

    public void RecalculateContentStatistics()
    {
        var posts = _contentService.GetAllPosts();

        Data.BlogPosts = posts.Count(x => x.Metadata is
            { Published: true, Type: PostType.Normal }
        );

        Data.StaticPages = posts.Count(x => x.Metadata is
            { Published: true, Type: PostType.Static }
        );

        Data.Drafts = posts.Count(x => !x.Metadata.Published);
        
        Data.DiskUsage = new DirectoryInfo(WellKnown.Content)
            .EnumerateFiles("*", SearchOption.AllDirectories)
            .Sum(file => file.Length);
        
        Save();
    }

    public void DashboardHit()
    {
        Data.DashboardViewCount++;
        Save();
    }

    public void PostHit(Guid postId)
    {
        Data.PostViews.TryAdd(postId, 0);
        Data.PostViews[postId]++;
        
        Save();
    }
    
    public void Save()
    {
        File.WriteAllText(
            FilePath,
            JsonConvert.SerializeObject(Data, Formatting.Indented)
        );
    }
}