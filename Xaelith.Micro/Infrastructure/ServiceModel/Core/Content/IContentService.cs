namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Xaelith.Micro.Infrastructure.DataModel.Core.Content;

public interface IContentService : IXaelithService
{
    List<Post> GetAllPosts(Predicate<Post>? filter = null);
    Post? GetPostBySlug(string slug);
    
    string GetCategoryDescription(string category);
    string GetTagDescription(string tag);
}