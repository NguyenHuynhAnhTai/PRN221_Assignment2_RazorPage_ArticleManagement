using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.ProfileManagement
{
    public class AccountDetailModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        public AccountDetailModel(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        public SystemAccount SystemAccount { get; set; } = default!;

        public string? Message { get; set; }

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount == null)
            {
                Message = "Not Found";
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }    

            var account = JsonSerializer.Deserialize<SystemAccount>(loginAccount);
            if (account != null)
            {
                SystemAccount = account;
                return Page();
            }

            Message = "Not Found";
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
