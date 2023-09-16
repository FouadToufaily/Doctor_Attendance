﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Sections
{
    public class DetailsModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public DetailsModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

      public Section Section { get; set; } = default!;
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userStatus = HttpContext.Session.GetString("UserStatus");

            RoleId = await _context.Users // getting the Role Id
                   .Where(u => u.Username == userStatus)
                   .Select(u => u.RoleId)
                   .FirstOrDefaultAsync();

            if (RoleId != null)
            {
                RoleName = await _context.Roles
                    .Where(r => r.RoleId == RoleId)
                    .Select(r => r.RoleName)
                    .FirstOrDefaultAsync();
            }

            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FirstOrDefaultAsync(m => m.Sectionid == id);
            if (section == null)
            {
                return NotFound();
            }
            else 
            {
                Section = section;
            }
            return Page();
        }
    }
}
