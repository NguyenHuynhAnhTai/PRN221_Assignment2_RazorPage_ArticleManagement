using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Services.Implementations;
using BusinessObjects;
using NguyenHuynhAnhTaiRazorPages.Models;
using System.Text.Json;

namespace NguyenHuynhAnhTaiRazorPages.Pages.Login
{
    public class LoginPageModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        [BindProperty]
        public LoginAccount LoginAccount { get; set; } = default!;

        public string ErrorMessage { get; set; }

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
                LoginAccount.AccountId = systemAccount.AccountId;
                LoginAccount.AccountName = systemAccount.AccountName;
                LoginAccount.AccountRole = systemAccount.AccountRole;

                jsonLoginAccount = JsonSerializer.Serialize(LoginAccount, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true
                });
                if (systemAccount.AccountRole == 1)
                {
                    StaticUserInformation.UserInfo = systemAccount;
                    return RedirectToPage("/ProfileManagement");
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

        public IActionResult OnPostViewNews()
        {
            return RedirectToPage("/ViewNews");
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
