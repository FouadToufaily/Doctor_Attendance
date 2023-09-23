using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DetailsModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

      public Employee Employee { get; set; } = default!;
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

            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.EmpId == id);
            var dep = await _context.Departments.FirstOrDefaultAsync(m => m.DepId == employee.DepId);
            employee.Dep = dep;

            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;
            }
            return Page();
        }
    }
}
