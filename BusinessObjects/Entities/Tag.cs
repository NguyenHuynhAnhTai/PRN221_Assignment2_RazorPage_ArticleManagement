using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entities;

public partial class Tag
{
    [BindProperty]
    [Required(ErrorMessage = "Tag Id is required.")]
    public int TagId { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Tag Name is required.")]
    public string? TagName { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Tag Note is required.")]
    public string? Note { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
