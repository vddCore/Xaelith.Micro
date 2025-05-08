namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Xaelith.Micro.Infrastructure.DataModel.Admin.Editor;
using Xaelith.Micro.Infrastructure.DataModel.Core.Content;

public interface IContentService : IXaelithService
{
    List<Post> GetAllPosts(Predicate<Post>? filter = null);
    
    Post? GetPostBySlug(string slug);
    Post? GetPostById(string id);
    Post? GetPostById(Guid id);
    
    string GetCategoryDescription(string category);
    string GetTagDescription(string tag);
    
    void DeletePost(Guid postId);
    Task SavePostAsync(EditorContext context);
    Task SetPublishedStateAsync(Guid postId, bool isPublished);

    string GenerateSlug(string title);
    
    Task<bool> UploadPostMediaAsync(Guid postId, Stream stream, string fileName);
    List<(string FullPath, string MappedPath)> GetPostMedia(Guid postId);
    bool DeletePostMedia(Guid postId, string fileName);
}