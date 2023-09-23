using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public RegisterInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                if (_context.Users.Any(u => u.Username == Input.Username))
                {
                    ModelState.AddModelError("Input.Username", "Username already exists.");
                    return Page();
                }

                // Create a new user based on the input
                var user = new User
                {
                    Username = Input.Username,
                    Password = Input.Password,
                    DateCreated = DateTime.Now,
                    LastModified = DateTime.Now
                };        
                     if (Input.Role.Equals("Secratary"))
                
                    user.RoleId = 1;
                     else if (Input.Role.Equals("Head Of Department"))
                      user.RoleId = 2;
                     else if (Input.Role.Equals("Head Of Faculty"))
                      user.RoleId = 3;
                     else if (Input.Role.Equals("Head Of Section"))
                      user.RoleId = 4; 


                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToPage("../../Index"); // Redirect to the Index page
            }

            return Page();
        }

        public class RegisterInputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }
        }
    }
}