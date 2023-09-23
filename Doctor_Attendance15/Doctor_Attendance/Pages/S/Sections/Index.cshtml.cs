using System;
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
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IList<Section> Section { get;set; } = default!;
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public async Task OnGetAsync()
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
            if (_context.Sections != null)
            {
                Section = await _context.Sections
                .Include(s => s.Doctor).Include(f => f.Faculties).ToListAsync();
            }
        }
    }
}
