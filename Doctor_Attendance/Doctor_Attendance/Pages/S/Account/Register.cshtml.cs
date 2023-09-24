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

        public List<Doctor> Doctors { get; set; } // Add this property to fetch doctors
        public List<Employee> Employees { get; set; } // Add this property to fetch employees

        public void OnGet()
        {
            // Initialize Input with default values
            Input = new RegisterInputModel();

            // Fetch the list of doctors and employees
            Doctors = _context.Doctors.ToList();
            Employees = _context.Employees.ToList();
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

                if (Input.Role.Equals("Secretary"))
                {
                    user.RoleId = 1;
                    // Set the EmpId for Secretary role
                    user.EmpId = Input.DoctorEmployeeId;
                }
                else if (Input.Role.Equals("Head Of Department"))
                {
                    user.RoleId = 2;
                    // Set the DoctorId for Head Of Department role
                    user.DoctorId = Input.DoctorEmployeeId;
                }
                else if (Input.Role.Equals("Head Of Faculty"))
                {
                    user.RoleId = 3;
                    // Set the DoctorId for Head Of Faculty role
                    user.DoctorId = Input.DoctorEmployeeId;
                }
                else if (Input.Role.Equals("Head Of Section"))
                {
                    user.RoleId = 4;
                    // Set the DoctorId for Head Of Section role
                    user.DoctorId = Input.DoctorEmployeeId;
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToPage("../../Index"); // Redirect to the Index page
            }

            // Fetch the list of doctors and employees again and assign them to Doctors and Employees properties.
            Doctors = _context.Doctors.ToList();
            Employees = _context.Employees.ToList();

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

            // Add this property to store the selected doctor or employee ID
            public int DoctorEmployeeId { get; set; }
        }
    }
}
