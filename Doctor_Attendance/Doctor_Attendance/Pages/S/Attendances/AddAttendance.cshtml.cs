using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.ViewByDate
{
    [BindProperties]
    public class AddAttendanceModel : PageModel
    {

        private readonly Doctor_Attendance.Services.AppDBContext _context;
        public AddAttendanceModel(AppDBContext dbContext)
        {

            this._context = dbContext;
            AttendenceRecords = new List<Attendance>();
            Records = new List<Boolean>();
            department = new Department();
            employee = new Employee();
            //empId = 1; // removed this, set it in the onget
        }
        public int? RoleId { get; set; }
        public int? EmpId { get; set; }
        public int? DoctorId { get; set; }

        public string? RoleName { get; set; }
        public string? EmpDep { get; set; }

        public string? DoctorDep { get; set; }

        [BindProperty]
        public List<Attendance> AttendenceRecords { get; set; }

        [BindProperty]
        public List<Boolean> Records { get; set; }

        [BindProperty]
        public IList<Doctor> Doctors { get; set; } = default!;
        public IList<Department> Departements { get; set; } = default!;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime datev { get; set; }

        public Department department { get; set; }

        [BindProperty]
        public int empId { get; set; }

        public Employee employee { get; set; } = default!;

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

                empId = EmpId ?? 1;
            }
          
            if (_context.Doctors != null)
            {
                Doctors = await _context.Doctors
                .Include(d => d.Category).ToListAsync();
            }
            foreach (var doctor in Doctors)
            {
                Attendance attendence = new Attendance();
                attendence.DoctorId = doctor.DoctorId; // Assign the correct DoctorId
                AttendenceRecords.Add(attendence);

                Boolean b = new Boolean();
                Records.Add(b);
            }
            // Get the employee with determined empId
            var emp = await _context.Employees.FirstOrDefaultAsync(e => e.EmpId == empId);
            if (emp == null)
                return;
            employee = emp;
            // Get the department with determined depId
            var dep = await _context.Departments.FirstOrDefaultAsync(d => d.DepId == employee.DepId);
            if (dep == null)
                return;
            department = dep;
        }

        public IActionResult OnPost()
        {
            /*
            if (!ModelState.IsValid || dbContext.Attendances == null )
            {
                return Page();
            }
            */
            // Assign atttended=1 to checked Records
            int i = 0;
            for (i = 0; i < Records.Count; i++)
            {
                if (Records[i] == true)
                    AttendenceRecords[i].Attended = true;
            }
            var checkedRecords = AttendenceRecords.Where(r => r.Attended == true).ToList();
            if (checkedRecords.Count == 0)
                return NotFound();

            _context.Attendances.AddRange(checkedRecords);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public void GetUser()
        {

        }
    }
}