using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.TagManagement
{
    public class DetailsModel : PageModel
    {
        private readonly ITagService _tagService;

        public DetailsModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        public Tag Tag { get; set; } = default!;

        public string? Message { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            if (id == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }

            var tag = _tagService.GetTags().FirstOrDefault(m => m.TagId == id);
            if (tag == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }
            else
            {
                Tag = tag;
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
