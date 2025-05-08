namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using System.Globalization;
using Newtonsoft.Json;
using Slugify;
using Xaelith.Micro.Infrastructure.DataModel.Admin.Editor;
using Xaelith.Micro.Infrastructure.DataModel.Core;
using Xaelith.Micro.Infrastructure.DataModel.Core.Content;
using Xaelith.Micro.Infrastructure.Utilities;

public class ContentService : IContentService
{
    private readonly SlugHelper _slugHelper;
    private readonly IConfigService _configService;

    public ContentService(IConfigService configService)
    {
        _slugHelper = new SlugHelper();
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

        return collection.Count != 0 
            ? collection.Single() 
            : null;
    }

    public Post? GetPostById(Guid id)
    {
        var collection = GetAllPosts(p => p.Id == id);
        
        return collection.Count != 0 
            ? collection.Single() 
            : null;
    }

    public Post? GetPostById(string id)
    {
        return Guid.TryParse(id, out var postId) 
            ? GetPostById(postId)
            : null;
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

    public void DeletePost(Guid postId)
    {
        var postRoot = Path.Combine(
            WellKnown.Content,
            postId.ToString("D")
        );
        
        if (Directory.Exists(postRoot))
            Directory.Delete(postRoot, true);
    }

    public async Task SavePostAsync(EditorContext context)
    {
        var postDirectory = Path.Combine(
            WellKnown.Content,
            context.Id.ToString("D")
        );

        var postMediaDirectory = Path.Combine(
            postDirectory,
            WellKnown.PostMediaDirectoryName
        );
        
        Directory.CreateDirectory(postDirectory);
        Directory.CreateDirectory(postMediaDirectory);

        var postBodyPath = Path.Combine(
            postDirectory,
            WellKnown.PostBodyFileName
        );

        await using (var sw = new StreamWriter(postBodyPath)) 
            await sw.WriteAsync(context.Markdown);

        var postMetaPath = Path.Combine(
            postDirectory,
            WellKnown.PostMetadataFileName
        );
        
        PostMetadata? postMeta = null;
        
        if (File.Exists(postMetaPath))
        {
            using (var sr = new StreamReader(postMetaPath))
            {
                postMeta = JsonConvert.DeserializeObject<PostMetadata>(
                    await sr.ReadToEndAsync()
                );
            }
        }

        postMeta ??= new PostMetadata();
        
        postMeta.Title = context.Title;
        postMeta.Slug = context.Slug;
        postMeta.Description = context.Description;
        postMeta.Author = context.EditingUser;
        postMeta.Category = context.Category;
        postMeta.Tags = context.TagList.Split(
            ',',
            StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries
        ).ToList();
        postMeta.EditDate = DateTime.Now;

        if (string.IsNullOrWhiteSpace(context.PublishDateString))
        {
            if (!postMeta.PublishDate.HasValue && context.IsPublished)
                postMeta.PublishDate = DateTime.Now;
        }
        else
        {
            if(DateTime.TryParseExact(
                context.PublishDateString,
                _configService.Root!.General.DateFormat,
                null,
                DateTimeStyles.None,
                out var publishDate
            ))
            {
                postMeta.PublishDate = publishDate;
            }
            else
            {
                postMeta.PublishDate = DateTime.Now;
            }
        }

        postMeta.Published = context.IsPublished;

        if (string.IsNullOrWhiteSpace(postMeta.Slug))
        {
            postMeta.Slug = GenerateSlug(context.Title);
        }

        postMeta.Type = context.PostType;
        
        await using(var sw = new StreamWriter(postMetaPath))
            await sw.WriteAsync(JsonConvert.SerializeObject(postMeta));
    }

    public async Task SetPublishedStateAsync(Guid postId, bool isPublished)
    {
        var postMetaPath = Path.Combine(
            WellKnown.Content,
            postId.ToString("D"),
            WellKnown.PostMetadataFileName
        );

        PostMetadata? postMeta;

        using (var sr = new StreamReader(postMetaPath))
        {
            postMeta = JsonConvert.DeserializeObject<PostMetadata>(
                await sr.ReadToEndAsync()
            );

            if (postMeta == null)
                return;

            if (!postMeta.PublishDate.HasValue && isPublished)
            {
                postMeta.PublishDate = DateTime.Now;
            }

            postMeta.Published = isPublished;
        }

        await using (var sw = new StreamWriter(postMetaPath))
        {
            await sw.WriteAsync(
                JsonConvert.SerializeObject(postMeta)
            );
        }
    }

    public string GenerateSlug(string title)
    {
        var slugBase = _slugHelper.GenerateSlug(title);
        
        if (string.IsNullOrWhiteSpace(slugBase))
            return string.Empty;

        var slug = slugBase;
        var existingSlugs = GetAllPosts(p => p.Metadata.Slug.StartsWith(slugBase))
            .Select(p => p.Metadata.Slug)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        int i = 1;
        while (existingSlugs.Contains(slug))
        {
            slug = $"{slugBase}-{i++}";
        }

        return slug;
    }

    public async Task<bool> UploadPostMediaAsync(Guid postId, Stream stream, string fileName)
    {
        try
        {
            var mediaDirectory = Path.Combine(
                WellKnown.Content,
                postId.ToString("D"),
                WellKnown.PostMediaDirectoryName
            );

            Directory.CreateDirectory(mediaDirectory);

            var mediaFilePath = Path.Combine(
                mediaDirectory,
                fileName
            );
            
            if (File.Exists(mediaFilePath))
                File.Delete(mediaFilePath);

            using (var fs = File.OpenWrite(mediaFilePath))
                await stream.CopyToAsync(fs);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public List<(string FullPath, string MappedPath)> GetPostMedia(Guid postId)
    {
        var mediaDirectory = Path.Combine(
            WellKnown.Content,
            postId.ToString("D"),
            WellKnown.PostMediaDirectoryName
        );
        
        Directory.CreateDirectory(mediaDirectory);
        
        var list = new List<(string FullPath, string MappedPath)>();

        foreach (var fullPath in Directory.GetFiles(mediaDirectory))
        {
            list.Add((
                fullPath, 
                fullPath.Replace(WellKnown.Content, "").Replace("\\", "/")
            ));
        }

        return list;
    }

    public bool DeletePostMedia(Guid postId, string fileName)
    {
        var mediumPath = Path.Combine(
            WellKnown.Content,
            postId.ToString("D"),
            WellKnown.PostMediaDirectoryName,
            fileName
        );

        if (!File.Exists(mediumPath))
            return false;
        
        File.Delete(mediumPath);
        return true;
    }
}