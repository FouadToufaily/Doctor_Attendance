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
        public IList<Attendance> Attendances { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; } = default!;

        public int? RoleId { get; set; }
        public int? EmpId { get; set; }
        public int? DoctorId { get; set; }
        public string? RoleName { get; set; }
        public string? EmpDep { get; set; }

        public string? DoctorDep { get; set; }
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
            DoctorId = await _context.Users
                .Where(u => u.Username == userStatus)
                .Select(u => u.DoctorId)
                .FirstOrDefaultAsync();

            DoctorDep = await _context.Doctors
                .Where(u => u.DoctorId == DoctorId)
                .Select(u => u.Dep.DepName)
                .FirstOrDefaultAsync();

            if (!DoctorId.HasValue)
            {
                EmpId = await _context.Users
                    .Where(u => u.Username == userStatus)
                    .Select(u => u.EmpId)
                    .FirstOrDefaultAsync();
                EmpDep = await _context.Employees
                    .Where(u => u.EmpId == EmpId)
                    .Select(u => u.Dep.DepName)
                    .FirstOrDefaultAsync();
            }
            if (EmpDep != null)
            {
                Attendances = await _context.Attendances.Include(a => a.Dep)
                    .Include(a => a.Doctor)
                    .Where(a => a.Dep.DepName.Equals(EmpDep))
                  .ToListAsync();
            }
            else if (DoctorDep != null)
            {
                if (RoleName.Equals("HOD"))
                {
                    Attendances = await _context.Attendances.Include(a => a.Dep)
                        .Include(a => a.Doctor)
                        .Where(a => a.Dep.DepName.Equals(DoctorDep))
                        .ToListAsync();
                }
                else
                {
                    Attendances = await _context.Attendances.Include(a => a.Dep)
                        .Include(a => a.Doctor).ToListAsync();
                }

            }
            else
            {
                Attendances = await _context.Attendances.Include(a => a.Dep)
                    .Include(a => a.Doctor).ToListAsync();
            }
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

