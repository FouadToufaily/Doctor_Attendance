using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Doctor_Attendance.Pages.New
{
    public class EditAttendanceModel : PageModel
    {
        private readonly AppDBContext dbContext;
        public EditAttendanceModel(AppDBContext dbContext)
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
        [BindProperty(SupportsGet = true)]
        public string name { get; set; }

        public int secrataryDepId { get; set; }
        public async Task<IActionResult> OnGetAsync(string? date, string? name)
        {
            //get username of the current secratary
            var username = HttpContext.Session.GetString("UserStatus");

            //get current employee
            Employee employee = dbContext.Employees.FirstOrDefault(e => e.Email.Equals(username));

            //get secrataryDepId
            secrataryDepId = (int)employee.DepId;

            DateTime selectedDate;
            if (DateTime.TryParse(date, out selectedDate))
            {
                // Check if the selected date is a disabled date
                if (IsDateDisabled(selectedDate))
                {
                    datev = GetLastValidDate(selectedDate);
                }
                else
                {
                    // The selected date is valid and not disabled, so set it to datev
                    datev = selectedDate;
                }

            }
            else
            {
                datev = DateTime.Now;
                return Page();
            }
            if (dbContext.Doctors != null)
            {
                Doctor = await dbContext.Doctors
                    .Include(d => d.Category)
                    .Where(d =>d.DepId == secrataryDepId)
                    .ToListAsync();
            }

            // Fetch all Attendance records for the specified targetDate
            var attendancesForDate = await dbContext.Attendances
                .Where(a => a.Date == datev)
                .ToListAsync();

            AttendanceRecords = new List<Attendance>();
            bool isAnyRecordPublished = await dbContext.Attendances.AnyAsync(a => a.Date == datev && a.Published == true);

            if (isAnyRecordPublished)
            {
                if (dbContext.Attendances == null)
                {
                    return NotFound();
                }
                foreach (var doctor in Doctor)
                {
                    // Find the attendance record for the doctor on the specified date
                    Attendance attendance = attendancesForDate.FirstOrDefault(a => a.DoctorId == doctor.DoctorId);

                    if (attendance != null)
                    {
                        // Add the attendance record to the list if it exists and is published for the doctor on the specified date
                        AttendanceRecords.Add(attendance);
                    }
                }
            }
            else
            {
                foreach (var doctor in Doctor)
                {
                    Attendance attendance = attendancesForDate.FirstOrDefault(a => a.DoctorId == doctor.DoctorId);

                    if (attendance == null)
                    {
                        // Create a new attendance record if it doesn't exist for the doctor on the specified date
                        attendance = new Attendance()
                        {
                            DoctorId = doctor.DoctorId,
                            Date = datev
                        };
                    }
                    else
                    {
                        // Find the corresponding attendance record in AttendanceRecords
                        var attendanceRecord = AttendanceRecords.FirstOrDefault(a => a.DoctorId == doctor.DoctorId);

                        if (attendanceRecord != null)
                        {
                            // Update the properties for the specific attendance record that matches the doctor
                            attendanceRecord.NbHours = attendance.NbHours;
                            attendanceRecord.Comments = attendance.Comments;
                            attendanceRecord.Attended = attendance.Attended;
                         //   attendanceRecord.AttId = attendance.AttId;  //this
                        }
                    }
                    // Add the attendance record to the list (whether it's new or existing)
                    AttendanceRecords.Add(attendance);
                }
            }
            if (name == "Day")
            {
                TempData["Message"] = datev;
                // Update the "Published" attribute to true for all records with the selected date
                var attendancesToUpdate = dbContext.Attendances
                .Where(a => a.Date == datev)
                .ToList();

                foreach (var attendance in attendancesToUpdate)
                {
                    attendance.Published = true;
                }

                try
                {
                    dbContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Handle the exception and provide appropriate feedback to the user
                    // For example, log the error and show a friendly error message
                    // return RedirectToAction("Error", "Home");
                }
            }
            else if (name == "Month")
            {
                // Get the first day of the selected month
                DateTime firstDayOfMonth = new DateTime(datev.Year, datev.Month, 1);

                // Update the "Published" attribute to true for all records within the selected date range
                var attendancesToUpdate = dbContext.Attendances
                    .Where(a => a.Date >= firstDayOfMonth && a.Date <= datev)
                    .ToList();

                foreach (var attendance in attendancesToUpdate)
                {
                    attendance.Published = true;
                }

                try
                {
                    dbContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Handle the exception and provide appropriate feedback to the user
                    // For example, log the error and show a friendly error message
                    // return RedirectToAction("Error", "Home");
                }

            }

            return Page();

        }
        private bool IsDateDisabled(DateTime date)
        {
            // Define the disabled dates in the format "MM/dd"
            var disabledDates = new List<string> { "11/22", "06/01", "12/10", "12/25" };
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            // Check if the current date is disabled
            var formattedDate = date.ToString("MM/dd");
            return disabledDates.Contains(formattedDate);
        }

        private DateTime GetLastValidDate(DateTime currentDate)
        {
            while (true)
            {
                // Check if the current date is disabled or falls on a weekend
                if (!IsDateDisabled(currentDate) && currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    return currentDate;
                }

                // Decrement the current date by one day
                currentDate = currentDate.AddDays(-1);
            }
        }

        public IActionResult OnPostNext()
        {

            return RedirectToPage("EditAttendance", new { date = datev, name = "View" });

        }

        public IActionResult OnPostSave()
        {
            // Separate the checked and unchecked records
            var checkedRecords = AttendanceRecords.Where(r => r.Attended == true).ToList();
            var uncheckedRecords = AttendanceRecords.Where(r => r.Attended == false).ToList();

            // Get the existing records for the checked records
            var existingCheckedRecords = dbContext.Attendances
                .Where(a => checkedRecords.Select(c => c.AttId).Contains(a.AttId))
                .ToList();

            // Update existing checked records with the new values
            foreach (var existingRecord in existingCheckedRecords)
            {
                var updatedRecord = checkedRecords.FirstOrDefault(c => c.AttId == existingRecord.AttId);
                if (updatedRecord != null)
                {
                    // Update the existing record properties
                    existingRecord.NbHours = updatedRecord.NbHours;
                    existingRecord.Comments = updatedRecord.Comments;
                    // No need to update "Attended" as it remains true
                }
            }

            // Add new checked records (assuming AttId is auto-incremented in the database)
            var newCheckedRecords = checkedRecords.Where(c => !(existingCheckedRecords.Select(e => e.AttId).Contains(c.AttId)));
            /*
            // Get the last AttId from the table
            int? lastAttId = dbContext.Attendances.Select(r => (int?)r.AttId).Max();

            // Assign incremented AttId values
            int attId = lastAttId.HasValue ? lastAttId.Value + 1 : 1;
            foreach (var record in newCheckedRecords)
            {
                record.AttId = attId;
                attId++; // Increment the AttId value
            }
            */
            dbContext.Attendances.AddRange(newCheckedRecords);
            bool hasUncheckedRecords = uncheckedRecords.Any();
            
            
            if (hasUncheckedRecords )
            {
                var existingRecordsToDelete = uncheckedRecords.Where(c => dbContext.Attendances.Select(e => e.AttId).Contains(c.AttId));
                // Remove unchecked records from the database
                dbContext.Attendances.RemoveRange(existingRecordsToDelete);
            }

            const int maxRetries = 3;
            int retryCount = 0;

            while (true)
            {
                try
                {
                    dbContext.SaveChanges();
                    break; // Break out of the loop if the save operation succeeds
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (++retryCount > maxRetries)
                    {
                        // Handle the retry limit being reached (optional)
                        throw new Exception("Retry limit reached.", ex);
                    }

                    // Reload the conflicting entities from the database and try again
                    foreach (var entry in ex.Entries)
                    {
                        entry.Reload();
                    }
                }


            }


            return RedirectToPage("EditAttendance", new { date = datev, name = "Save" });
        }


        public IActionResult OnPostThisDay()
        {
            return RedirectToPage("EditAttendance", new { date = datev, name = "Day" });
        }

        public IActionResult OnPostThisMonth()
        {
            return RedirectToPage("EditAttendance", new { date = datev, name = "Month" });
        }

    }
}



