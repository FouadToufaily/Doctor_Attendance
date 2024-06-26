﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Doctor_Attendance.Pages.S.AttendancesView
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public string RoleName1 { get; set; }

        public Doctor Doctor { get; set; } = default!;

        public IList<Attendance> Attendance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string RoleName)
        {
            RoleName1 = RoleName;
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            Doctor = doctor;

            if (_context.Attendances != null)
            {
                Attendance = await _context.Attendances.Where(a=>a.Doctor.DoctorId == id).ToListAsync();
            }

            return Page();

        }
    }
}
