using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.NewsArticleManagement
{
    public class DetailsModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public DetailsModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public NewsArticle NewsArticle { get; set; } = default!;

        public string? Message { get; set; }

        public IActionResult OnGet(string id)
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            if (id == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }

            var newsarticle = _newsArticleService.GetNewsArticles().FirstOrDefault(m => m.NewsArticleId == id);
            if (newsarticle == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }
            else
            {
                NewsArticle = newsarticle;
            }
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
