namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Xaelith.Micro.Infrastructure.DataModel.Content;

public interface IContentService : IXaelithService
{
    List<(Guid Id, Post Post)> GetAllPosts(Predicate<Post>? filter = null);
    string GetPostBody(Guid postId);
}