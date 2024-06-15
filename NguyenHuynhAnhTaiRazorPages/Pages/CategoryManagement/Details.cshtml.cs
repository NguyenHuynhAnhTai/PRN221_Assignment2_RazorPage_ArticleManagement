using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.CategoryManagement
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DetailsModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Category Category { get; set; } = default!;

        public string? Message { get; set; }

        public IActionResult OnGet(short? id)
        {
            if(!CheckSession())
                return RedirectToPage("/LoginPage");

            if (id == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }

            var category = _categoryService.GetCategories().FirstOrDefault(m => m.CategoryId == id);
            if (category == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }
            else
            {
                Category = category;
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
