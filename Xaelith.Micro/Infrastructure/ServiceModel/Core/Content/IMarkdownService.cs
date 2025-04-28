namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Content;

public interface IMarkdownService : IXaelithService
{
    string Render(string markdown);
    string RenderBrief(string markdown, int length);
}