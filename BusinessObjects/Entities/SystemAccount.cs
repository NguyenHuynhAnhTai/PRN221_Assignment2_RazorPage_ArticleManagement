using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entities;

public partial class SystemAccount
{
    public short AccountId { get; set; }

    public string? AccountName { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[A-Za-z][\w\-\.]*@FUNewsManagement[\w\-\.]*\.org$",
                            ErrorMessage = "Email is invalid!\n" +
                                            "Format email: Word + .... + @FUNewsManagement + .. + .org")]
    public string? AccountEmail { get; set; }

    public int? AccountRole { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(2, ErrorMessage = "Password must be at least 2 words")]
    public string? AccountPassword { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
