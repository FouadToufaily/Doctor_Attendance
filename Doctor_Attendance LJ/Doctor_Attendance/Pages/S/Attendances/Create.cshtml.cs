using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Attendances
{
    public class CreateModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public CreateModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepId");
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            return Page();
        }

        [BindProperty]
        public Attendance Attendance { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Attendances == null || Attendance == null)
            {
                return Page();
            }

            _context.Attendances.Add(Attendance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
