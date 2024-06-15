using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.TagManagement
{
    public class IndexModel : PageModel
    {
        private readonly ITagService _tagService;

        public IndexModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            Tag = _tagService.GetTags();
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
