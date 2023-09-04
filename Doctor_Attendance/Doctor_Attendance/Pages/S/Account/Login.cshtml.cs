using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Doctor_Attendance.Pages.S.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDBContext _context;

        public LoginModel(AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string LogInValid { get; set; } // for error messages
        public string Username { get; set; }
        public IActionResult OnGet()
        {
            Username = "";
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            LogInValid = ""; // Reset the error message

            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == Input.Username))
                {
                    if (_context.Users.Any(u => u.Username == Input.Username && !(u.Password == Input.Password)))
                    {
                        LogInValid = "Incorrect Password";
                    }
                    else
                    {
                        Username = Input.Username;
                        return RedirectToPage("../../Index", new { Username = Input.Username + "" });
                    }
                }
                else
                {
                    LogInValid = "This username does not exist";
                }
            }

            return Page();
        }

        public class LoginInputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }
}
