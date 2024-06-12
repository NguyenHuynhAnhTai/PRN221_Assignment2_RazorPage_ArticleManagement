using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NguyenHuynhAnhTaiRazorPages.Models
{
    public class LoginAccount
    {
        public short? AccountId { get; set; } = default!;

        public string? AccountName { get; set; } = default!;

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[A-Za-z][\w\-\.]*@FUNewsManagement[\w\-\.]*\.org$",
                            ErrorMessage = "Email is invalid!\n" +
                                            "Format email: Word + .... + @FUNewsManagement + .. + .org")]
        public string AccountEmail { get; set; }

        public int? AccountRole { get; set; } = default!;

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(2, ErrorMessage = "Password must be at least 2 words")]
        public string AccountPassword { get; set; }
    }
}
