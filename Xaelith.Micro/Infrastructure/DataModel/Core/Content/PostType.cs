namespace Xaelith.Micro.Infrastructure.DataModel.Core.Content;

using System.ComponentModel.DataAnnotations;

public enum PostType
{
    [Display(Name = "Blog Post")]
    Normal,
    
    [Display(Name = "Static Page")]
    Static
}