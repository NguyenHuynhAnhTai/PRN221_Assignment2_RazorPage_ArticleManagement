using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.NewsArticleManagement
{
    public class DeleteModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public string? Message { get; set; }

        public DeleteModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

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

            var newsarticle = _newsArticleService.GetNewsArticleById(id);

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

        public IActionResult OnPostDelete()
        {
            string? id = Request.Form["id"];
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            if (string.IsNullOrEmpty(id))
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }

            try
            {
                var newsarticle = _newsArticleService.GetNewsArticleById(id);
                if (newsarticle == null)
                {
                    Message = "Not Found";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();

                }

                NewsArticle = newsarticle;

                if (newsarticle.NewsStatus == false)
                {
                    Message = "This article had been deleted";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();
                }

                NewsArticle.NewsStatus = false;
                _newsArticleService.UpdateNewsArticle(NewsArticle);


                Message = "Delete successfully!";
                ModelState.AddModelError(string.Empty, Message);

                return Page();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }
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
