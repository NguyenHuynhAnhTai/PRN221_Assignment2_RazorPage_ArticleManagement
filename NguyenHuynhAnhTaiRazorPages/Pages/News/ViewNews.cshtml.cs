using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NguyenHuynhAnhTaiRazorPages.Pages.News
{
    public class ViewNewsModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public ViewNewsModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IList<NewsArticle> NewsArticle { get;set; } = default!;

        public IActionResult OnGet()
        {
            NewsArticle = _newsArticleService.GetNewsArticles().Where(a => a.NewsStatus == true).ToList();
            return Page();
        }
    }
}
