using System;
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
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public int? RoleId { get; set; }
        public int? EmpId { get; set; }
        public int? DoctorId { get; set; }

        public string? RoleName { get; set; }
        public string? EmpDep { get; set; }

        public string? DoctorDep { get; set; }


        public IEnumerable<Doctor> Doctor { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = default!;
        public string message { get; set; }

        public async Task OnGetAsync()
        {
            var userStatus = HttpContext.Session.GetString("UserStatus");

            RoleId = await _context.Users // getting the Role Id
                   .Where(u => u.Username == userStatus)
                   .Select(u => u.RoleId)
                   .FirstOrDefaultAsync();

            RoleName = await _context.Roles //Extracting the Role name(might need it later because i dont know the Ids of the roles now
                .Where(r => r.RoleId == RoleId)
                .Select(r => r.RoleName)
                .FirstOrDefaultAsync();

            DoctorId = await _context.Users // checking if it's a doctor, if not this stays null
                  .Where(u => u.Username == userStatus)
                  .Select(u => u.DoctorId)
                  .FirstOrDefaultAsync();

            DoctorDep = await _context.Doctors //getting Doctor's Department
            .Where(u => u.DoctorId == DoctorId)
            .Select(u => u.Dep.DepName)
            .FirstOrDefaultAsync();

            if (!DoctorId.HasValue)
            {
                EmpId = await _context.Users // if it's a Secretary we get his/her ID
                 .Where(u => u.Username == userStatus)
                 .Select(u => u.EmpId)
                 .FirstOrDefaultAsync();

                EmpDep = await _context.Employees // Get her Department
                   .Where(u => u.EmpId == EmpId)
                   .Select(u => u.Dep.DepName)
                   .FirstOrDefaultAsync();
            }

            if (_context.Doctors != null)
            {
                if (EmpId.HasValue)
                {
                    Doctor = await _context.Doctors // use it here by Secretary Department
                        .Include(d => d.Category)
                        .Include(d => d.Dep)
                        .Where(d => d.Dep.DepName == EmpDep || d.Dep==null) // Filter doctors by department
                        .ToListAsync();
                }
                else if (DoctorId.HasValue)
                {
                    if (RoleName.Equals("HOD"))
                    {
                        Doctor = await _context.Doctors // Or here by Doctor Department
                            .Include(d => d.Category)
                            .Include(d => d.Dep)
                            .Where(d => d.Dep.DepName == DoctorDep) // Filter doctors by department
                            .ToListAsync();
                    }
                    else
                    {
                        Doctor = await _context.Doctors
                            .Include(d => d.Category).Include(d => d.Dep).ToListAsync();
                    }
                }
                else
                {
                    Doctor = await _context.Doctors
                        .Include(d => d.Category).Include(d => d.Dep).ToListAsync();
                }

            }
            if (RoleName.Equals("Admin") || RoleName.Equals("HOS") || RoleName.Equals("HOF"))
            {
                Doctor = _context.SearchDoctor(SearchString); // calling Search function by search criteria which is binded to the form control textbox
            }
            //else // searchdoctor should return only doctors of same dep and HODs and hos and hof 
            //{
            //    if (EmpDep != null)
            //    {
            //        Doctor = _context.SearchDoctor(SearchString, EmpDep); // calling Search function by search criteria which is binded to the form control textbox
            //    }
            //    else
            //    {
            //        Doctor = _context.SearchDoctor(SearchString, DoctorDep); // calling Search function by search criteria which is binded to the form control textbox
            //    }
            //}

        }

    }
}
