using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entities;

public partial class Category
{
    public short CategoryId { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Category Name is required.")]
    public string CategoryName { get; set; } = null!;

    [BindProperty]
    [Required(ErrorMessage = "Category Description is required.")]
    public string CategoryDesciption { get; set; } = null!;

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
