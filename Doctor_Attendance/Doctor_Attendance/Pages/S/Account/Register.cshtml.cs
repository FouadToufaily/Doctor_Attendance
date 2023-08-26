using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDBContext _context;

        public RegisterModel(AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; } = new RegisterInputModel();

        public class RegisterInputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            public int SelectedRoleId { get; set; } // Added role selection

            // Other properties if needed
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Create a new user
                var newUser = new User
                {
                    Username = Input.Username,
                    Password = Input.Password,
                    RoleId = Input.SelectedRoleId // Use the selected role ID
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Redirect to the index page or perform other actions
                return RedirectToPage("/S/Test/Index");
            }

            // If ModelState is invalid, stay on the page and show the validation errors
            return Page();
        }
    }
}
