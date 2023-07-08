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
        public Attendence Attendence { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Attendences == null)
            {
                return NotFound();
            }

            var attendence =  await _context.Attendences.FirstOrDefaultAsync(m => m.AttId == id);
            if (attendence == null)
            {
                return NotFound();
            }
            Attendence = attendence;
           ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepId");
           ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Attendence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendenceExists(Attendence.AttId))
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
          return (_context.Attendences?.Any(e => e.AttId == id)).GetValueOrDefault();
        }
    }
}
