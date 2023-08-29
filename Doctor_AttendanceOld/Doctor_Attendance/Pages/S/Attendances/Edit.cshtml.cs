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

namespace Doctor_Attendance.Pages.S.Attendances
{
    public class EditModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public EditModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Attendance Attendance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendence =  await _context.Attendances.FirstOrDefaultAsync(m => m.AttId == id);
            if (attendence == null)
            {
                return NotFound();
            }
            Attendance = attendence;
           ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName");
           ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Fullname");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors); // To see all the errors in model state

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendenceExists(Attendance.AttId))
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

        private bool AttendenceExists(int id)
        {
          return (_context.Attendances?.Any(e => e.AttId == id)).GetValueOrDefault();
        }
    }
}
