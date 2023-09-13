using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Holidays
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }
        public string RoleName { get; set; }
        public IList<Holiday> Holiday { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //Get RoleID
            var roleIdStr = HttpContext.Session.GetString("RoleId");

            //Get RoleName
            if (!string.IsNullOrEmpty(roleIdStr))
            {
                int roleId = Convert.ToInt32(roleIdStr);
                RoleName = _context.Roles
                                   .Where(r => r.RoleId == roleId)
                                   .Select(r => r.RoleName)
                                   .FirstOrDefault();
            }

            if (_context.Holidays != null)
            {
                Holiday = await _context.Holidays.ToListAsync();
            }
        }
    }
}
