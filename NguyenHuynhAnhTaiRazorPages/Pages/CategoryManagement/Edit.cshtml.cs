using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;
using Services.Implementations;

namespace NguyenHuynhAnhTaiRazorPages.Pages.CategoryManagement
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public EditModel(ICategoryService categoryService)
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
            if (id != null)
            {
                var category = _categoryService.GetCategories().FirstOrDefault(m => m.CategoryId == id);
                if (category == null)
                {
                    Message = "Not Found";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();
                }
                Category = category;
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            var id = Request.Form["id"];
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _categoryService.Add(Category);


                    Message = "Create successfully!";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();
                }

                var category = _categoryService.GetCategories().FirstOrDefault(c => c.CategoryId.ToString() == id);
                if (category == null)
                {
                    Message = "Not Found";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();

                }

                _categoryService.Update(Category);

                Message = "Update successfully!";
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
