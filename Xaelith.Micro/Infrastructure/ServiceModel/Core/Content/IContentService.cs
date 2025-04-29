namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Xaelith.Micro.Infrastructure.DataModel.Content;

public interface IContentService : IXaelithService
{
    List<(Guid Id, Post Post)> GetAllPosts(Predicate<Post>? filter = null);
    (Guid Id, Post Post)? GetPostBySlug(string slug);
    
    string GetPostBody(Guid postId);

    string GetCategoryDescription(string category);
    string GetTagDescription(string tag);
}