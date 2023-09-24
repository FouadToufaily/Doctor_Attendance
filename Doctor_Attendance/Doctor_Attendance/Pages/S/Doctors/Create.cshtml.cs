using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Doctors
{
    public class CreateModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;
        public string? newFileNb { get; set; }
        public CreateModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            // Get the last lastFileNb from the table
            string lastFileNb = _context.Doctors.OrderByDescending(d => d.DoctorId).Select(d => d.FileNumber).First();
            string[] fileNbArray = lastFileNb.Split('D');
            int fileNb = Int32.Parse( fileNbArray[fileNbArray.Length - 1]);
            fileNb++;
            newFileNb = "D" + fileNb;
            Console.WriteLine(fileNb);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Type");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepId", "DepName");
            return Page();
        }

        [BindProperty]
        public Doctor Doctor { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Doctors == null || Doctor == null)
            {
                return Page();
            }

            _context.Doctors.Add(Doctor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
