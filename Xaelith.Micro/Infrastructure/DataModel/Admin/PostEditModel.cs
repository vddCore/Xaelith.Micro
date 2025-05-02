namespace Xaelith.Micro.Infrastructure.DataModel.Admin;

using System.ComponentModel.DataAnnotations;

public class PostEditModel
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string Content { get; set; } = string.Empty;
}