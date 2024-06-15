using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;
using Services.Implementations;
using System.ComponentModel.DataAnnotations;

namespace NguyenHuynhAnhTaiRazorPages.Pages.NewsArticleManagement
{
    public class EditModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ISystemAccountService _systemAccountService;
        private readonly ITagService _tagService;

        public string? Message { get; set; }

        public EditModel(INewsArticleService newsArticleService, ICategoryService categoryService, ISystemAccountService systemAccountService, ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _systemAccountService = systemAccountService;
            _tagService = tagService;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        public List<int> SelectedTagIds { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        public IActionResult OnGet(string id)
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            if (id != null)
            {
                var newsarticle = _newsArticleService.GetNewsArticles().FirstOrDefault(m => m.NewsArticleId == id);
                if (newsarticle != null)
                {
                    NewsArticle = newsarticle;
                    Status = NewsArticle.NewsStatus is true ? "Active" : "Inactive";
                    SelectedTagIds = NewsArticle.Tags.Select(t => t.TagId).ToList();
                }
                else
                {
                    Message = "Article does not exist";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();
                }
            }

            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryDesciption");
            ViewData["CreatedById"] = new SelectList(_systemAccountService.GetAccounts(), "AccountId", "AccountName");
            ViewData["Tags"] = new MultiSelectList(_tagService.GetTags(), "TagId", "TagName", SelectedTagIds);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            string? id = Request.Form["id"];
            if (!CheckSession())
                return RedirectToPage("/LoginPage");


            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    NewsArticle.NewsArticleId = Guid.NewGuid().ToString().Substring(0, 20);
                    if (Status.Trim().ToLower() == "active")
                    {
                        NewsArticle.NewsStatus = true;
                    }
                    else
                    {
                        NewsArticle.NewsStatus = false;
                    }
                    NewsArticle.CreatedDate = DateTime.Now;
                    NewsArticle.CreatedById = JsonSerializer.Deserialize<SystemAccount>(HttpContext.Session.GetString("LoginSession")).AccountId;

                    _newsArticleService.SaveNewsArticle(NewsArticle);


                    Message = "Create successfully!";
                    ModelState.AddModelError(string.Empty, Message);

                    ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryDesciption");
                    ViewData["CreatedById"] = new SelectList(_systemAccountService.GetAccounts(), "AccountId", "AccountName");
                    ViewData["Tags"] = new MultiSelectList(_tagService.GetTags(), "TagId", "TagName", SelectedTagIds);

                    return Page();
                }

                var newsarticle = _newsArticleService.GetNewsArticleById(id);
                if (newsarticle == null)
                {
                    Message = "Not Found";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();

                }
                NewsArticle.CreatedDate = newsarticle.CreatedDate;
                NewsArticle.ModifiedDate = DateTime.Now;
                NewsArticle.CreatedById = newsarticle.CreatedById;
                if (Status.Trim().ToLower() == "active")
                {
                    NewsArticle.NewsStatus = true;
                }
                else
                {
                    NewsArticle.NewsStatus = false;
                }

                _newsArticleService.UpdateNewsArticle(NewsArticle);

                Message = "Update successfully!";
                ModelState.AddModelError(string.Empty, Message);

                ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryDesciption");
                ViewData["CreatedById"] = new SelectList(_systemAccountService.GetAccounts(), "AccountId", "AccountName");
                ViewData["Tags"] = new MultiSelectList(_tagService.GetTags(), "TagId", "TagName", SelectedTagIds);

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
