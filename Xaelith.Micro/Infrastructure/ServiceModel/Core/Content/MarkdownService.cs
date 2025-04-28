namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

using Markdig;
using Markdig.Extensions.Emoji;

public class MarkdownService : IMarkdownService
{
    private IConfigService _configService;
    private MarkdownPipeline _markdownPipeline;
    
    public MarkdownService(IConfigService configService)
    {
        _configService = configService;
        
        _markdownPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Use(new EmojiExtension(EmojiMapping.DefaultEmojisOnlyMapping))
            .Build();
    }

    public string Render(string markdown)
    {
        var pbToken = _configService.Root!.Rendering.PageBreakToken;

        if (!string.IsNullOrWhiteSpace(pbToken))
            markdown = markdown.Replace(pbToken, string.Empty);
        
        return Markdown.ToHtml(
            markdown,
            _markdownPipeline
        );
    }

    public string RenderBrief(string markdown, int length)
    {
        markdown = markdown.Substring(0, length)
            .Trim()
            .TrimEnd('.');

        markdown += "<span class=\"post-continuation-prompt\">...</span>";
        return Render(markdown);
    }
}