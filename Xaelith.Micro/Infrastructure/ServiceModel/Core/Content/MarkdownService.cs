namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Markdig;
using Markdig.Extensions.Emoji;

public class MarkdownService : IMarkdownService
{
    private MarkdownPipeline _markdownPipeline;
    
    public MarkdownService()
    {
        _markdownPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Use(new EmojiExtension(EmojiMapping.DefaultEmojisOnlyMapping))
            .Build();
    }

    public string Render(string markdown)
    {
        return Markdown.ToHtml(
            markdown,
            _markdownPipeline
        );
    }

    public string RenderBrief(string markdown, int length)
    {
        markdown = markdown.Substring(0, length);

        if (!markdown.EndsWith(".")
            && !markdown.EndsWith("\n"))
        {
            markdown += "...";
        }

        return Render(markdown);
    }
}