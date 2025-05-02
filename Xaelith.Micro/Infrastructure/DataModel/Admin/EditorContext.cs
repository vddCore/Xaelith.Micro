namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

using System.ComponentModel.DataAnnotations;

public class EditorContext
{
    private string _markdown = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string TagList { get; set; } = string.Empty;

    [Required]
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

    public string PreviewMarkup { get; set; } = string.Empty;
    public int Lines { get; set; } = 1;
    public int Characters { get; set; }
}