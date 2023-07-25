using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Faculties
{
    public class DeleteModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DeleteModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Faculty Faculty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FirstOrDefaultAsync(m => m.Facultyid == id);

            if (faculty == null)
            {
                return NotFound();
            }
            else 
            {
                Faculty = faculty;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }
            var faculty = await _context.Faculties.FindAsync(id);

            if (faculty != null)
            {
                Faculty = faculty;
                _context.Faculties.Remove(Faculty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
