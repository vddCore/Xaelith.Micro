namespace Xaelith.Micro.Infrastructure.DataModel.Admin.Editor;

using Xaelith.Micro.Infrastructure.DataModel.Core.Content;

public class EditorContext
{
    private string _markdown = string.Empty;
    private PostType _postType = PostType.Normal;

    public Guid Id { get; set; } = Guid.NewGuid();

    public PostType PostType
    {
        get => _postType;
        set
        {
            _postType = value;

            if (_postType == PostType.Static)
            {
                Category = string.Empty;
                TagList = string.Empty;
            }
        }
    }

    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string TagList { get; set; } = string.Empty;

    public string Markdown
    {
        get => _markdown;
        set
        {
            _markdown = value;

            Lines = _markdown.Count(c => c == '\n') + 1;
            Characters = _markdown.Length;
        }
    }

    public string EditingUser { get; set; } = "nobody";
    public bool IsPublished { get; set; }

    public string PreviewMarkup { get; set; } = string.Empty;
    
    public int Lines { get; set; } = 1;
    public int Characters { get; set; }
}