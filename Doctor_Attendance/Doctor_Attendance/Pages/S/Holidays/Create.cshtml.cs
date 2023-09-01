using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Holidays
{
    public class CreateModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;
        public string ValidStr  { get; set; }
        public CreateModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Holiday Holiday { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Holidays == null || Holiday == null)
            {
                return Page();
            }

            var dateExists = _context.Holidays.Any(h => h.Date.Date == Holiday.Date.Date);
            if(!dateExists)
            {
                _context.Holidays.Add(Holiday);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            else
            {
                ValidStr = "A holiday already exists at this date.";
                return Page();

            }
            

            
        }
    }
}
