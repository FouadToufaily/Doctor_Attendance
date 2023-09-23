using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using System.Numerics;

namespace Doctor_Attendance.Pages.S.Attendances
{
    public class DeleteModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DeleteModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Attendance Attendance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, DateTime? Date)//error happening because primary key of attendance is id and date, i need to change it in database for it to become just id, or fix it here, Fix : Date is now sent from index razor page as asp route attribute Date, and find async in post can now find the Attendance record according to the double key id and Date
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendence = await _context.Attendances.FirstOrDefaultAsync(m => m.AttId == id && m.Date == Date);
            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == attendence.DoctorId);
            attendence.Doctor = doctor;
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepId == attendence.DepId);
            attendence.Dep = department;
            if (attendence == null)
            {
                return NotFound();
            }
            else 
            {
                Attendance = attendence;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, DateTime? Date) // date is null, i have to fix it<Fouad> Fix : from index razor page i included date as asp route attribute attribute along with the id and captured the date in the get mehtod above
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }
            var attendence = await _context.Attendances.FindAsync(id,Date);

            if (attendence != null)
            {
                Attendance = attendence;
                _context.Attendances.Remove(Attendance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
