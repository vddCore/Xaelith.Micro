namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.Core;
using Xaelith.Micro.Infrastructure.DataModel.Core.Content;
using Xaelith.Micro.Infrastructure.Utilities;

public class ContentService : IContentService
{
    private readonly IConfigService _configService;

    public ContentService(IConfigService configService)
    {
        _configService = configService;
        
        Directory.CreateDirectory(WellKnown.Content);
    }

    public List<(Guid Id, DataModel.Core.Content.Post Post)> GetAllPosts(Predicate<DataModel.Core.Content.Post>? filter = null)
    {
        var postDirectories = Directory.GetDirectories(WellKnown.Content);
        var posts = new List<(Guid Id, DataModel.Core.Content.Post Post)>();
        
        foreach (var postRoot in postDirectories)
        {
            var directoryName = Path.GetFileName(postRoot);
            
            if (!Guid.TryParse(directoryName, out var postId))
                continue;
            
            var metadataPath = Path.Combine(
                postRoot, 
                WellKnown.PostMetadataFileName
            );
            
            var bodyPath = Path.Combine(
                postRoot, 
                WellKnown.PostContentFileName
            );
            
            if (!File.Exists(metadataPath)) 
                continue;

            if (!File.Exists(bodyPath))
                continue;

            try
            {
                using var sr = new StreamReader(metadataPath);
                    
                var metadata = JsonConvert.DeserializeObject<Post>(
                    sr.ReadToEnd()
                );
                
                if (metadata == null)
                    continue;

                if (!filter?.Invoke(metadata) ?? false)
                    continue;
                    
                posts.Add((
                    postId,
                    metadata
                ));

                var direction = _configService.Root!.General.PostOrderDirection;
                
                posts = _configService.Root!.General.PostOrderCriteria switch
                {
                    PostOrderCriteria.Date =>
                        direction == PostOrderDirection.Ascending
                            ? posts.OrderBy(x => x.Post.PublishDate).ToList()
                            : posts.OrderByDescending(x => x.Post.PublishDate)
                                .ToList(),
                    PostOrderCriteria.Alphabetical =>
                        direction == PostOrderDirection.Ascending
                            ? posts.OrderBy(x => x.Post.Title).ToList()
                            : posts.OrderByDescending(x => x.Post.Title)
                                .ToList(),
                    _ => posts
                };
            }
            catch
            {
                /* Just skip the corrupt post for now. */
            }
        }

        return posts;
    }

    public (Guid Id, Post Post)? GetPostBySlug(string slug)
    {
        var collection = GetAllPosts(p => p.Slug == slug);

        if (collection.Count == 0)
            return null;
        
        return collection.Single();
    }

    public string GetPostBody(Guid postId)
    {
        var postBodyPath = Path.Combine(
            WellKnown.Content, 
            postId.ToString("D"),
            WellKnown.PostContentFileName
        );

        if (!File.Exists(postBodyPath))
            return string.Empty;
        
        using var sr = new StreamReader(postBodyPath);
        return sr.ReadToEnd();
    }

    public string GetCategoryDescription(string category)
    {
        return _configService.Root!.Content.Categories.TryGetValue(
            category, out var categoryDescription
        ) ? categoryDescription 
          : string.Empty;
    }

    public string GetTagDescription(string tag)
    {
        return _configService.Root!.Content.Tags.TryGetValue(
            tag, out var tagDescription
        ) ? tagDescription 
          : string.Empty;
    }
}