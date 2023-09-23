using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Section = Doctor_Attendance.Models.Section;

namespace Doctor_Attendance.Pages.S.Attendances
{
    [BindProperties]
    public class ViewAttendanceModel : PageModel
    {

        private readonly AppDBContext dbContext;
        public ViewAttendanceModel(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
            AttendanceRecords = new List<Attendance>();
            Holidays = GetHolidaysFromDatabase();
        }

        [BindProperty]
        public List<Attendance> AttendanceRecords { get; set; }
        [BindProperty]
        public IList<Doctor> Doctor { get; set; } = default!;
        [BindProperty]
        public IList<Department> Department { get; set; } = default!;

        [BindProperty]
        public Faculty Faculty { get; set; } = default!;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime datev { get; set; }
        [BindProperty]
        public int FacultyId { get; set; }
        public int SectionId { get; set; }
        public int DrId { get; set; }
        public int secrataryDepId { get; set; }

        public string? RoleName { get; set; }

        public int? RoleId { get; set; }
        public List<string> Holidays { get; private set; }
        public IList<Faculty> Facultyl { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? date)
        {
            var username = HttpContext.Session.GetString("UserStatus");

            RoleId = await dbContext.Users // getting the Role Id
                   .Where(u => u.Username == username)
                   .Select(u => u.RoleId)
                   .FirstOrDefaultAsync();

            RoleName = await dbContext.Roles //Extracting the Role name(might need it later because i dont know the Ids of the roles now
                .Where(r => r.RoleId == RoleId)
                .Select(r => r.RoleName)
                .FirstOrDefaultAsync();

            if (RoleName.Equals("HOD"))
            {

                Doctor doctor = dbContext.Doctors.FirstOrDefault(d => d.Email.Equals(username));
                if (dbContext.Doctors != null)
                {
                    Doctor = await dbContext.Doctors
                        .Include(d => d.Category)
                        .Where(d => d.DepId == doctor.DepId)
                        .ToListAsync();
                }
            }

            else if (RoleName.Equals("HOF"))
            {
                Doctor doctor = dbContext.Doctors.FirstOrDefault(d => d.Email.Equals(username));

                if (doctor != null)
                {
                    DrId = doctor.DoctorId;
                    Faculty faculty1 = dbContext.Faculties.FirstOrDefault(f => f.DoctorId.Equals(DrId));

                    if (faculty1 != null)
                    {
                        FacultyId = faculty1.Facultyid;
                        var query = dbContext.Departments
                            .Where(d => d.Faculties.Any(f => f.Facultyid == FacultyId));

                        var departments = await query.ToListAsync();

                        List<int> depIds = departments.Select(d => d.DepId).ToList();

                        Doctor = await dbContext.Doctors
                .Include(d => d.Category)
                .Where(d => depIds.Contains((int)d.DepId))
                .ToListAsync();
                    }
                    else
                    {
                        TempData["Message"] = " the faculty was not found.";
                    }
                }

            }
            else if (RoleName.Equals("HOS"))
            {
                Doctor doctor = dbContext.Doctors.FirstOrDefault(d => d.Email.Equals(username));

                if (doctor != null)
                {
                    DrId = doctor.DoctorId;
                    Section section = dbContext.Sections.FirstOrDefault(f => f.DoctorId.Equals(DrId));

                    if (section != null)
                    {
                        SectionId = section.Sectionid;
                        var query = dbContext.Departments
    .Where(d => d.Faculties.Any(f => f.Sections.Any(s => s.Sectionid == SectionId)));

                        var departments = await query.ToListAsync();

                        List<int> depIds = departments.Select(d => d.DepId).ToList();

                        Doctor = await dbContext.Doctors
                .Include(d => d.Category)
                .Where(d => depIds.Contains((int)d.DepId))
                .ToListAsync();
                    }
                    else
                    {
                        TempData["Message"] = " the section was not found.";
                    }
                }

            }
            else
            {
                Doctor = null;
                TempData["Message"] = " the section was not found.";
            }
            DateTime selectedDate;
            if (DateTime.TryParse(date, out selectedDate))
            {
                datev = selectedDate;
            }
            else
            {
                datev = DateTime.Now;
            }
            if (dbContext.Attendances == null)
            {
                return NotFound();
            }

            if (Doctor != null && Doctor.Any()) // Check if Doctor is not null and has elements
            {
                var attendancesForDate = await dbContext.Attendances
                    .Where(a => a.Date == datev && a.Published == true)
                    .ToListAsync();

                foreach (var doctor in Doctor)
                {

                    Attendance attendance = attendancesForDate.FirstOrDefault(a => a.DoctorId == doctor.DoctorId);

                    if (attendance != null)
                    {
                        // Find the corresponding attendance record in AttendanceRecords
                        var attendanceRecord = AttendanceRecords.FirstOrDefault(a => a.DoctorId == doctor.DoctorId);

                        if (attendanceRecord != null)
                        {
                            // Update the properties for the specific attendance record that matches the doctor
                            attendanceRecord.NbHours = attendance.NbHours;
                            attendanceRecord.Comments = attendance.Comments;
                            attendanceRecord.Attended = attendance.Attended;
                        }
                        else
                        {
                            // Add the attendance record to the list if it's not already there
                            AttendanceRecords.Add(attendance);
                        }
                    }
                }
            }

            return Page();
        }


        public IActionResult OnPostNext()
        {

            return RedirectToPage("ViewAttendance", new { date = datev });

        }

        public IActionResult OnPostSave()
        {

            return RedirectToPage();
        }
        private List<string> GetHolidaysFromDatabase()
        {
            var holidayEntities = dbContext.Holidays.ToList();

            // Assuming the 'Date' property in your 'Holiday' entity stores the date in yyyy-mm-dd format 
            Holidays = holidayEntities.Select(h => h.Date.ToString("yyyy-MM-dd")).ToList();
            return Holidays;
        }
    }
}




