using Doctor_Attendance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Attendance.Pages.HOS
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public Faculty Faculty { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            //get doctorId of the current username 
            var username = HttpContext.Session.GetString("UserStatus");
            var id = _context.Doctors
                                   .Where(d => d.Email.Equals(username))
                                   .Select(d => d.DoctorId)
                                   .FirstOrDefault();

            if (id == null )
            {
                return NotFound();
            }
            var faculty = _context.Faculties
                                  .Include(f => f.Doctor)
                                  .Include(f => f.Sections)
                                  .Where(f => f.Sections.Any(s => s.DoctorId == id)).FirstOrDefault();
        
            if (faculty == null)
            {
                return NotFound();
            }
        
            Faculty = faculty;
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            return Page();
        }
    }
}
