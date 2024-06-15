using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.CategoryManagement
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Category> Category { get;set; } = default!;

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            Category = _categoryService.GetCategories();
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
