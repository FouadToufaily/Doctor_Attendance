﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Attendances
{
    public class DetailsModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DetailsModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

      public Attendence Attendence { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Attendences == null)
            {
                return NotFound();
            }

            var attendence = await _context.Attendences.FirstOrDefaultAsync(m => m.AttId == id);
            if (attendence == null)
            {
                return NotFound();
            }
            else 
            {
                Attendence = attendence;
            }
            return Page();
        }
    }
}
