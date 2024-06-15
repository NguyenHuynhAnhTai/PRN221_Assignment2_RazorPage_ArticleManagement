using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;

namespace NguyenHuynhAnhTaiRazorPages.Pages.News
{
    public class NewsDetailModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public NewsDetailModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public NewsArticle NewsArticle { get; set; } = default!;

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = _newsArticleService.GetNewsArticles().FirstOrDefault(m => m.NewsArticleId == id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }
    }
}
