using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Doctor_Attendance.Pages.New
{
    public class ViewAttendanceModel : PageModel
    {

        private readonly AppDBContext dbContext;
        public ViewAttendanceModel(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
            AttendanceRecords = new List<Attendance>();
        }

        [BindProperty]
        public List<Attendance> AttendanceRecords { get; set; }

        public IList<Doctor> Doctor { get; set; } = default!;
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime datev { get; set; }




        public async Task<IActionResult> OnGetAsync(string? date)
        {
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

            if (dbContext.Doctors != null)
            {
                Doctor = await dbContext.Doctors
                    .Include(d => d.Category)
                    .ToListAsync();
            }

            // Fetch all Attendance records for the specified targetDate
            var attendancesForDate = await dbContext.Attendances
                .Where(a => a.Date == datev)
                .ToListAsync();

            // Now you have the list of Attendance records for the targetDate

            AttendanceRecords = new List<Attendance>();

            foreach (var doctor in Doctor)
            {
                // Find the attendance record for the doctor on the specified date
                Attendance attendance = attendancesForDate.FirstOrDefault(a => a.DoctorId == doctor.DoctorId && a.Published == true);

                if (attendance != null)
                {
                    // Add the attendance record to the list if it exists and is published for the doctor on the specified date
                    AttendanceRecords.Add(attendance);
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






    }
}




