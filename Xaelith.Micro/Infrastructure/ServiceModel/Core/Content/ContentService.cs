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

    public List<Post> GetAllPosts(Predicate<Post>? filter = null)
    {
        var postDirectories = Directory.GetDirectories(WellKnown.Content);
        var posts = new List<Post>();
        
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
                WellKnown.PostBodyFileName
            );
            
            if (!File.Exists(metadataPath)) 
                continue;

            if (!File.Exists(bodyPath))
                continue;

            try
            {
                using var sr = new StreamReader(metadataPath);
                    
                var metadata = JsonConvert.DeserializeObject<PostMetadata>(
                    sr.ReadToEnd()
                );
                
                if (metadata == null)
                    continue;

                var post = new Post(
                    postId,
                    metadata,
                    postRoot,
                    _configService.Root!.Rendering.PageBreakToken
                );

                if (!filter?.Invoke(post) ?? false)
                    continue;
                    
                posts.Add(post);
            }
            catch
            {
                /* Just skip the corrupt post for now. */
            }
        }
        
        var direction = _configService.Root!.General.PostOrderDirection;
                
        posts = _configService.Root!.General.PostOrderCriteria switch
        {
            PostOrderCriteria.Date =>
                direction == PostOrderDirection.Ascending
                    ? posts.OrderBy(x => x.Metadata.PublishDate).ToList()
                    : posts.OrderByDescending(x => x.Metadata.PublishDate)
                        .ToList(),
            PostOrderCriteria.Alphabetical =>
                direction == PostOrderDirection.Ascending
                    ? posts.OrderBy(x => x.Metadata.Title).ToList()
                    : posts.OrderByDescending(x => x.Metadata.Title)
                        .ToList(),
            _ => posts
        };
        
        return posts;
    }

    public Post? GetPostBySlug(string slug)
    {
        var collection = GetAllPosts(p => p.Metadata.Slug == slug);

        if (collection.Count == 0)
            return null;
        
        return collection.Single();
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