using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.Text.Json;
using BusinessObjects.Entities;

namespace NguyenHuynhAnhTaiRazorPages.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        [BindProperty]
        public SystemAccount LoginAccount { get; set; } = default!;

        public string? ErrorMessage { get; set; }

        public LoginPageModel(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }



        public IActionResult OnPostLogin()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var adminInfo = GetAdminInfo();

            SystemAccount? systemAccount = _systemAccountService.GetAccountByEmailAndPass(LoginAccount.AccountEmail, LoginAccount.AccountPassword);

            string jsonLoginAccount;

            if (adminInfo.adminEmail.Equals(LoginAccount.AccountEmail) && adminInfo.adminPassword.Equals(LoginAccount.AccountPassword))
            {
                jsonLoginAccount = JsonSerializer.Serialize(LoginAccount, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true
                });
                HttpContext.Session.SetString("LoginSession", jsonLoginAccount);
                return RedirectToPage("/Admin");
            }
            if (systemAccount is not null)
            {
                jsonLoginAccount = JsonSerializer.Serialize(systemAccount, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true
                });
                if (systemAccount.AccountRole == 1)
                {
                    HttpContext.Session.SetString("LoginSession", jsonLoginAccount);
                    return RedirectToPage("/NewsArticleManagement/Index");
                }
                else
                {
                    ErrorMessage = "You do not have permission to login.";
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                    return Page();
                }
            }
            ErrorMessage = "Email or Password is incorrect";
            ModelState.AddModelError(string.Empty, ErrorMessage);
            return Page();
        }

        private (string adminEmail, string adminPassword) GetAdminInfo()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            string adminEmail = configuration["admin:email"];
            string adminPassword = configuration["admin:password"];
            return (adminEmail, adminPassword);
        }
    }
}
