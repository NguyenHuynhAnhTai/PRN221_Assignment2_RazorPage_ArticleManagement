using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.NewsArticleManagement
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public IndexModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;
        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            NewsArticle = _newsArticleService.GetNewsArticles();
            return Page();
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<SystemAccount>(loginAccount);
                if (account != null && account.AccountRole == 1)
                    return true;
            }
            return false;
        }
    }
}
