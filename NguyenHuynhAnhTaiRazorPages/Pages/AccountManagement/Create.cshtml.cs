using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Services.Interfaces;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace NguyenHuynhAnhTaiRazorPages.Pages.AccountManagement
{
    public class CreateModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        public CreateModel(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public string? Message { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a Role")]
        public string Role { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/LoginPage");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var account = _systemAccountService.GetAccounts().FirstOrDefault(a => a.AccountId == SystemAccount.AccountId);
                if (account != null)
                {
                    Message = "Account ID already exists";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();
                }

                if (Role.Trim().ToLower() == "staff")
                    SystemAccount.AccountRole = 1;
                else if (Role.Trim().ToLower() == "lecturer")
                    SystemAccount.AccountRole = 2;
                else
                {
                    Message = "Role must be either 'Staff' or 'Lecturer'";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();
                }

                _systemAccountService.AddAccount(SystemAccount);

                Message = "Account created successfully";
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
                if (account != null && account.AccountRole == -1)
                    return true;
            }
            return false;
        }
    }
}
