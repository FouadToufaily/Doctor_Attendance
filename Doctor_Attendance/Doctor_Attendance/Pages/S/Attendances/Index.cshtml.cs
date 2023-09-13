using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Doctor_Attendance.Pages.S.Attendances
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }
        public string RoleName { get; set; }
        public IList<Attendance> Attendances { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; } = default!;
        [BindProperty]
        public string s { get; set; }
        [BindProperty]
        public string s1 { get; set; }

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

            Attendances = await _context.Attendances.Include(a => a.Dep)
                     .Include(a => a.Doctor).ToListAsync();
            Attendances = _context.SearchAttendance(SearchString).ToList<Attendance>();
        }
        public IActionResult OnPostPublish(int attendanceToPublishId)
        {
            var att = _context.Attendances.FirstOrDefault(a => a.AttId == attendanceToPublishId);
            if (att is not null)
            att.Published = true;
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }

    }
}

