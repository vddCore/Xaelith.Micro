namespace Xaelith.Micro.Infrastructure.DataModel.Core.Content;

using Xaelith.Micro.Infrastructure.Utilities;

public record Post
{
    public Guid Id { get; set; }
    public PostMetadata Metadata { get; set; }

    public string RootDirectoryDirectory { get; set; }

    public string PageBreakToken { get; set; }
    public int PageBreakIndex { get; set; }

    public bool HasPageBreak
    {
        get
        {
            FetchBody();
            return PageBreakIndex >= 0;
        }
    }

    public string BodyPath => Path.Combine(
        RootDirectoryDirectory,
        WellKnown.PostBodyFileName
    );

    public string Body => FetchBody();

    public Post(Guid id, PostMetadata metadata, string rootDirectory, string pageBreakToken)
    {
        Id = id;
        Metadata = metadata;
        RootDirectoryDirectory = rootDirectory;
        PageBreakToken = pageBreakToken;
    }

    private string FetchBody()
    {
        var body = string.Empty;

        if (File.Exists(BodyPath))
        {
            try
            {
                using var sr = new StreamReader(BodyPath);
                body = sr.ReadToEnd();
            }
            catch
            {
                body = string.Empty;
            }
        }

        if (!string.IsNullOrWhiteSpace(PageBreakToken))
        {
            PageBreakIndex = body.IndexOf(
                PageBreakToken,
                StringComparison.InvariantCulture
            );
        }
        else
        {
            PageBreakIndex = -1;
        }
        
        return body;
    }
}