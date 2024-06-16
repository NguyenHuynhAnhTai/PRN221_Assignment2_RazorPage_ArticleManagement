using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.ReportStatistic
{
    public class NewsReportModel : PageModel
    {
        private readonly INewsArticleService _newsService;

        public NewsReportModel(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        [BindProperty]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime? StartDate { get; set; }

        [BindProperty]
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }

        public List<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            if (StartDate.HasValue && EndDate.HasValue && ModelState.IsValid)
            {
                NewsArticles = _newsService.GetNewsArticles()
                    .Where(n => n.CreatedDate >= StartDate && n.CreatedDate <= EndDate)
                    .OrderByDescending(n => n.CreatedDate)
                    .ToList();

                return Page();
            }
            return Page();
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<SystemAccount>(loginAccount);
                if (account != null && account.AccountRole == -1)
                    return true;
            }
            return false;
        }
    }
}

