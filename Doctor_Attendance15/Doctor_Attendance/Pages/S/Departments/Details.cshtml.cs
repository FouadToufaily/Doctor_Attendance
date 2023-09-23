using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DetailsModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

      public Department Department { get; set; } = default!;
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userStatus = HttpContext.Session.GetString("UserStatus");

            RoleId = await _context.Users // getting the Role Id
                   .Where(u => u.Username == userStatus)
                   .Select(u => u.RoleId)
                   .FirstOrDefaultAsync();

            if (RoleId != null)
            {
                RoleName = await _context.Roles
                    .Where(r => r.RoleId == RoleId)
                    .Select(r => r.RoleName)
                    .FirstOrDefaultAsync();
            }

            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepId == id);
            if (department == null)
            {
                return NotFound();
            }
            else 
            {
                Department = department;
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == Department.DoctorId);
            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                Department.Doctor = doctor;
            }
            return Page();
        }
    }
}
