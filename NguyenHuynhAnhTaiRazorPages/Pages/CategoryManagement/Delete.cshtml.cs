using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.CategoryManagement
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public string? Message { get; set; }

        public IActionResult OnGet(short? id)
        {
            if (!CheckSession())
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

        public IActionResult OnPost()
        {
            string? id = Request.Form["id"];

            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var category = _categoryService.GetCategories().FirstOrDefault(c => c.CategoryId.ToString() == id);
            if (category == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }

            Category = category;
            _categoryService.Delete(Category);

            Message = "Delete successfully!";
            ModelState.AddModelError(string.Empty, Message);

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
