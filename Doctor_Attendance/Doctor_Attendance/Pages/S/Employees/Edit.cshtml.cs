using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Employees
{
    public class EditModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public EditModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.EmpId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
