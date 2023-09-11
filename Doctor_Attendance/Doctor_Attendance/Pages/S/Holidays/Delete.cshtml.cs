using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Holidays
{
    public class DeleteModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DeleteModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Holiday Holiday { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Holidays == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays.FirstOrDefaultAsync(m => m.ID == id);

            if (holiday == null)
            {
                return NotFound();
            }
            else 
            {
                Holiday = holiday;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Holidays == null)
            {
                return NotFound();
            }
            var holiday = await _context.Holidays.FindAsync(id);

            if (holiday != null)
            {
                Holiday = holiday;
                _context.Holidays.Remove(Holiday);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
