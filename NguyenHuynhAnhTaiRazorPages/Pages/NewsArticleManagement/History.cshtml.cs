using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.NewsArticleManagement
{
    public class HistoryModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public HistoryModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            NewsArticle = _newsArticleService.GetNewsArticles().Where(a => a.CreatedById == GetSession()?.AccountId).OrderByDescending(a => a.CreatedDate).ToList();
            return Page();
        }

        public SystemAccount? GetSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<SystemAccount>(loginAccount);
                if (account != null)
                    return account;
            }
            return null;
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
