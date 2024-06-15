using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusinessObjects.Entities;

public partial class NewsArticle
{
    [AllowNull]
    public string NewsArticleId { get; set; } = null!;

    [BindProperty]
    [Required(ErrorMessage = "News Title is required")]
    public string? NewsTitle { get; set; }

    public DateTime? CreatedDate { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "News Content is required")]
    public string? NewsContent { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Please select a category")]
    public short? CategoryId { get; set; }

    public bool? NewsStatus { get; set; }

    public short? CreatedById { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual SystemAccount? CreatedBy { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public string NewsStatusDisplay => NewsStatus is true? "Active" : "Inactive";
}
