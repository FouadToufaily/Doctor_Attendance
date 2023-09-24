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
        public int? EmpDep { get; set; }
        public Doctor? doctor { get; set; }
        public string? DoctorDep { get; set; }
        public int FacultyId { get; set; }
        public int SectionId { get; set; }
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
            //getting dr's Id
            DoctorId = await _context.Users
                .Where(u => u.Username == userStatus)
                .Select(u => u.DoctorId)
                .FirstOrDefaultAsync();

            //getting dr's dep
            DoctorDep = await _context.Doctors
                .Where(u => u.DoctorId == DoctorId)
                .Select(u => u.Dep.DepName)
                .FirstOrDefaultAsync();

            // if it's not a dr, then it's an employee
            if (!DoctorId.HasValue)
            {
                //getting empID
                EmpId = await _context.Users
                    .Where(u => u.Username == userStatus)
                    .Select(u => u.EmpId)
                    .FirstOrDefaultAsync();

                //getting emDep
                EmpDep = await _context.Employees
                    .Where(u => u.EmpId == EmpId)
                    .Select(u => u.DepId)
                    .FirstOrDefaultAsync();

                // getting attendance with dep of the emp
                if (EmpDep != null)
                {
                    var Attendances1 = _context.SearchAttendance(SearchString);
                    Attendances = Attendances1.Where(a => a.DepId == EmpDep)
                                              .ToList<Attendance>();
                }
                if (!EmpId.HasValue)//no docId and no empId the it is an admin
                {
                    if (RoleName.Equals("Admin"))
                    {
                        //Retrieve all attendances
                        Attendances = _context.SearchAttendance(SearchString).ToList<Attendance>();
                        
                    }

                }
            }

            else if (DoctorDep != null)//it is a doctor 
            {
                doctor = _context.Doctors.FirstOrDefault(d => d.Email.Equals(userStatus));

                if (RoleName.Equals("HOS"))
                {
                    Section section = _context.Sections.FirstOrDefault(s => s.DoctorId == DoctorId);

                    if (section != null)
                    {
                        SectionId = section.Sectionid;
                        var query = _context.Departments
                                             .Where(d => d.Faculties
                                             .Any(f => f.Sections
                                             .Any(s => s.Sectionid == SectionId)));

                        var departments = await query.ToListAsync();

                        List<int> depIds = departments.Select(d => d.DepId).ToList();

                        var Attendances1 = _context.SearchAttendance(SearchString);
                        Attendances = Attendances1.Where(a => a.Published == true && depIds.Contains((int)a.DepId))
                                                  .ToList<Attendance>();

                        

                    }
                }
                else if (RoleName.Equals("HOF"))
                {
                    Faculty faculty1 = _context.Faculties.FirstOrDefault(f => f.DoctorId == DoctorId);

                    if (faculty1 != null)
                    {
                        FacultyId = faculty1.Facultyid;
                        var query = _context.Departments
                                            .Where(d => d.Faculties
                                            .Any(f => f.Facultyid == FacultyId));

                        var departments = await query.ToListAsync();

                        List<int> depIds = departments.Select(d => d.DepId).ToList();

                        var Attendances1 = _context.SearchAttendance(SearchString);
                        Attendances = Attendances1.Where(a => a.Published == true && depIds.Contains((int)a.DepId))
                                                  .ToList<Attendance>();
                    }
                }
                else if (RoleName.Equals("HOD"))
                {
                    var Attendances1 = _context.SearchAttendance(SearchString);
                    Attendances = Attendances1.Where(a => a.Published == true && a.DepId == doctor.DepId)
                                              .ToList<Attendance>();
                }
            }
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

