using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.AttendancesTest
{
    public class DetailsModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DetailsModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }


        public Attendance Attendance { get; set; } = default!;

        [BindProperty]
        public string s { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendence = await _context.Attendances.FirstOrDefaultAsync(m => m.AttId == id);
            if (attendence is not null)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepId == attendence.DepId);
                if (department is not null)
                {
                    attendence.Dep = department;
                }

            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == attendence.DoctorId);
            attendence.Doctor = doctor;
            if (attendence == null)
            {
                return NotFound();
            }
            else
            {
                s += attendence.AttId.ToString() + " " + attendence.DepId + " " + attendence.Doctor.DoctorId;
                Attendance = attendence;
            }
            return Page();
        }
    }
}
