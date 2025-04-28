namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Xaelith.Micro.Infrastructure.DataModel.Content;

public interface IContentService : IXaelithService
{
    IReadOnlyDictionary<string, Post> Posts { get; }
    
    void RefreshPostCache();
    Dictionary<string, Post> GetAllPosts(Predicate<Post>? filter = null);
    string GetPostBody(Guid postId);
}