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

        private readonly AppDBContext dbContext;

        public AddAttendanceModel(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
            AttendenceRecords = new List<Attendance>();
            Records = new List<Boolean>();
            department = new Department();
            employee = new Employee();
            empId = 1;
        }

        [BindProperty]
        public List<Attendance> AttendenceRecords { get; set; }

        [BindProperty]
        public List<Boolean> Records { get; set; }

        [BindProperty]
        public IList<Doctor> Doctors { get; set; } = default!;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)] // Add this line
        public DateTime datev { get; set; }

        public Department department { get; set; }

        [BindProperty]
        public int empId { get; set; }

        public Employee employee { get; set; } = default!;

        [BindProperty]
        public string s { get; set; }

        public async Task OnGetAsync()
        {
                if (dbContext.Doctors != null)
            {
                Doctors = await dbContext.Doctors
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
            var emp = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmpId == empId);
            if (emp == null)
                return;
            employee = emp;
            // Get the department with determined depId
            var dep = await dbContext.Departments.FirstOrDefaultAsync(d => d.DepId == employee.DepId);
            if(dep == null)
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
            int i=0;
            for (i = 0; i < Records.Count; i++)
            {
                if (Records[i] == true)
                    AttendenceRecords[i].Attended = 1;
            } 
            var checkedRecords = AttendenceRecords.Where(r => r.Attended == 1).ToList();
            if(checkedRecords.Count == 0)
                return NotFound();

            dbContext.Attendances.AddRange(checkedRecords);
            dbContext.SaveChanges();

            return RedirectToPage();
        }
    }
}