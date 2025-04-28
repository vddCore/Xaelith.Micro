namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Newtonsoft.Json;
using Xaelith.Micro.Infrastructure.DataModel.Content;
using Xaelith.Micro.Infrastructure.Utilities;

public class ContentService : IContentService
{
    private Dictionary<string, Post> _posts;
    
    public IReadOnlyDictionary<string, Post> Posts => _posts;

    public ContentService()
    {
        Directory.CreateDirectory(WellKnown.Content);
        
        _posts = GetAllPosts();
    }

    public void RefreshPostCache()
        => _posts = GetAllPosts();

    public Dictionary<string, Post> GetAllPosts(Predicate<Post>? filter = null)
    {
        var postDirectories = Directory.GetDirectories(WellKnown.Content);
        var posts = new Dictionary<string, Post>();
        
        foreach (var postRoot in postDirectories)
        {
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
                    
                posts.Add(
                    Path.GetFileName(postRoot),
                    metadata
                );
            }
            catch
            {
                /* Just skip the corrupt post for now. */
            }
        }

        return posts;
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
}