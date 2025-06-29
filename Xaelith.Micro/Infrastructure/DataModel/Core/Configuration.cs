namespace Xaelith.Micro.Infrastructure.DataModel.Core;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xaelith.Micro.Infrastructure.DataModel.FrontEnd;

public record Configuration
{
    [JsonProperty("core")]
    public CoreSettings Core { get; private set; } = new();

    [JsonProperty("general")]
    public GeneralSettings General { get; private set; } = new();

    [JsonProperty("rendering")]
    public RenderingSettings Rendering { get; private set; } = new();

    [JsonProperty("navigation")]
    public NavigationSettings Navigation { get; private set; } = new();

    [JsonProperty("content")]
    public ContentSettings Content { get; private set; } = new();

    public Configuration Copy() => this with
    {
        Core = Core with { },
        General = General with { },
        Rendering = Rendering with { },
        Navigation = Navigation with { },
        Content = Content with { }
    };

    public void Replace(Configuration with)
    {
        var copy = with.Copy();

        Core = copy.Core with { };
        General = copy.General with { };
        Rendering = copy.Rendering with { };
        Navigation = copy.Navigation with { };
        Content = copy.Content with { };
    }
}

public record CoreSettings
{
    [JsonProperty("is_installed")]
    public bool IsInstalled { get; set; }

    [JsonProperty("max_file_upload_size_kb")]
    public int MaxFileUploadSizeKilobytes { get; set; } = 8192;

    [JsonProperty("autosave_interval_seconds")]
    public int AutosaveIntervalSeconds { get; set; } = 30;
    
    [JsonProperty("user_css")]
    public string UserCSS { get; set; } = string.Empty;
}

public record GeneralSettings
{
    [JsonProperty("site_title")]
    public string SiteTitle { get; set; } = "Xaelith Blog";

    [JsonProperty("site_description")]
    public string SiteDescription { get; set; } = "A blog powered by Xaelith";

    [JsonProperty("site_url")]
    public string SiteUrl { get; set; } = "https://localhost:5271";

    [JsonProperty("footer_text")]
    public string FooterText { get; set; } = "Copyright (c) 2025 Xaelith Project";

    [JsonProperty("date_format")]
    public string DateFormat { get; set; } = "dd MMMM yyyy - HH:mm";

    [JsonProperty("post_order_criteria")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PostOrderCriteria PostOrderCriteria { get; set; } = PostOrderCriteria.Date;

    [JsonProperty("post_order_direction")]
    [JsonConverter(typeof(StringEnumConverter))]
    public PostOrderDirection PostOrderDirection { get; set; } = PostOrderDirection.Descending;
}

public record RenderingSettings
{
    [JsonProperty("pagebreak_token")]
    public string PageBreakToken { get; set; } = "[[pagebreak]]";

    [JsonProperty("custom_regex_patterns")]
    public List<RegexPattern> CustomRegexPatterns { get; set; } = [];
}

public record NavigationSettings
{
    [JsonProperty("nav_lists")]
    public List<NavList> NavigationLists { get; set; } = [];
}

public record ContentSettings
{
    [JsonProperty("categories")]
    public Dictionary<string, string> Categories { get; set; } = [];

    [JsonProperty("tags")]
    public Dictionary<string, string> Tags { get; set; } = [];

    [JsonProperty("max_posts_per_page")]
    public int MaximumPostsPerPage { get; set; } = 7;

    [JsonProperty("max_pages_in_paginator")]
    public int MaximumPagesInPaginator { get; set; } = 7;
}