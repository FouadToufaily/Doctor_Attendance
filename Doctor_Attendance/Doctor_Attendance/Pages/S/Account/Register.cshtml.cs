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

        public List<Doctor> Doctors { get; set; }
        public List<Employee> Employees { get; set; }

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
            int roleId = 0; // Initialize roleId

            if (ModelState.IsValid)
            {
                // Check if the username already exists
                if (_context.Users.Any(u => u.Username == Input.Username))
                {
                    ModelState.AddModelError("Input.Username", "Username already exists.");
                    // Fetch the list of doctors and employees again and assign them to Doctors and Employees properties.
                    Doctors = _context.Doctors.ToList();
                    Employees = _context.Employees.ToList();
                    return Page();
                }

                // Check if a user already exists for the selected doctor or employee
                if (Input.Role == "Head Of Department" || Input.Role == "Head Of Faculty" || Input.Role == "Head Of Section")
                {
                    var existingUser = _context.Users.FirstOrDefault(u => u.DoctorId == Input.DoctorEmployeeId);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Input.Username", "This Doctor already has a username.");
                        // Fetch the list of doctors and employees again and assign them to Doctors and Employees properties.
                        Doctors = _context.Doctors.ToList();
                        Employees = _context.Employees.ToList();
                        return Page();
                    }
                    // Set the role ID based on the selected role
                    roleId = Input.Role switch
                    {
                        "Head Of Department" => 2,
                        "Head Of Faculty" => 3,
                        "Head Of Section" => 4, // Corrected role ID for Head Of Section
                        _ => throw new ArgumentException("Invalid role"),
                    };
                }
                else if (Input.Role == "Secretary")
                {
                    var existingUser = _context.Users.FirstOrDefault(u => u.EmpId == Input.DoctorEmployeeId);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Input.Username", "This Employee already has a username.");
                        // Fetch the list of doctors and employees again and assign them to Doctors and Employees properties.
                        Doctors = _context.Doctors.ToList();
                        Employees = _context.Employees.ToList();
                        return Page();
                    }
                    // Set the role ID for the Secretary
                    roleId = 1; // Assuming Secretary has role ID 1
                }

                // Create a new user based on the input
                var user = new User
                {
                    Username = Input.Username,
                    Password = Input.Password,
                    RoleId = roleId, // Set the role ID
                    DateCreated = DateTime.Now,
                    LastModified = DateTime.Now
                };

                // Set the DoctorId or EmployeeId based on the selected role
                if (Input.Role == "Head Of Department" || Input.Role == "Head Of Faculty" || Input.Role == "Head Of Section")
                {
                    user.DoctorId = Input.DoctorEmployeeId;
                }
                else if (Input.Role == "Secretary")
                {
                    user.EmpId = Input.DoctorEmployeeId;
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
