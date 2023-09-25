﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Doctors
{
    public class DetailsModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DetailsModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }
        public string? Rolename1 { get; set; }
        public Doctor Doctor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, string RoleName)
        {
            Rolename1 = RoleName;

            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == id);
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == doctor.CategoryId);
            doctor.Category = category;
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepId == doctor.DepId);
            doctor.Dep = department;
            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                Doctor = doctor;

            }
            return Page();
        }
    }
}
