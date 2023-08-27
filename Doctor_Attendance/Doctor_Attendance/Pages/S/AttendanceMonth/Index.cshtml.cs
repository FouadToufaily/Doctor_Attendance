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
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel.DataAnnotations;

namespace Doctor_Attendance.Pages.S.AttendanceMonth
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> Doctor { get; set; } = default!;
        public List<SelectListItem> DoctorItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MonthItems { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "January" },
            new SelectListItem { Value = "2", Text = "February" },
            new SelectListItem { Value = "3", Text = "March" },
            new SelectListItem { Value = "4", Text = "April" },
            new SelectListItem { Value = "5", Text = "May" },
            new SelectListItem { Value = "6", Text = "June" },
            new SelectListItem { Value = "7", Text = "July" },
            new SelectListItem { Value = "8", Text = "August" },
            new SelectListItem { Value = "9", Text = "September" },
            new SelectListItem { Value = "10", Text = "October" },
            new SelectListItem { Value = "11", Text = "November" },
            new SelectListItem { Value = "12", Text = "December" }
        };

        public bool ShowRecords { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedDoctor { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedMonth { get; set; }

        public List<Attendance> AttendanceRecords { get; set; }
        [BindProperty]
        public List<AttendanceInputModel> AttendanceInput { get; set; }

        public async Task OnGetAsync()
        {
            Doctor = await _context.Doctors.ToListAsync();
            DoctorItems = Doctor.Select(doctor => new SelectListItem
            {
                Value = doctor.DoctorId.ToString(),
                Text = $"{doctor.Firstname} {doctor.Lastname}"
            }).ToList();

            if (SelectedDoctor > 0 && SelectedMonth > 0)
            {
                var startDate = new DateTime(DateTime.Now.Year, SelectedMonth, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                AttendanceRecords = await _context.Attendances
                    .Where(a => a.DoctorId == SelectedDoctor && a.Date >= startDate && a.Date <= endDate)
                    .ToListAsync();

                ShowRecords = true;

                // Populate the AttendanceInput list with data from the database, if available
                AttendanceInput = Enumerable.Range(1, DateTime.DaysInMonth(startDate.Year, SelectedMonth))
                    .Select(day =>
                    {
                        var date = new DateTime(startDate.Year, SelectedMonth, day);
                        var existingAttendance = AttendanceRecords.FirstOrDefault(a => a.Date.Date == date.Date);

                        return new AttendanceInputModel
                        {
                            Date = date,
                            NbHours = existingAttendance?.NbHours ?? 0,
                            Attended = existingAttendance?.Attended,
                            Published = existingAttendance?.Published ?? false,
                            Comments = existingAttendance?.Comments
                        };
                    }).ToList();
            }
            else
            {
                ShowRecords = false;
                AttendanceInput = new List<AttendanceInputModel>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var startDate = new DateTime(DateTime.Now.Year, SelectedMonth, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                for (int currentDay = 1; currentDay <= DateTime.DaysInMonth(DateTime.Now.Year, SelectedMonth); currentDay++)
                {
                    var currentDate = new DateTime(DateTime.Now.Year, SelectedMonth, currentDay);
                    var attendanceInputForDay = new AttendanceInputModel
                    {
                        Date = currentDate,
                        NbHours = ConvertToInt32(Request.Form[$"AttendanceInput[{currentDay - 1}].NbHours"]),
                        Attended = Request.Form[$"AttendanceInput[{currentDay - 1}].Attended"] == "true",
                        Comments = Request.Form[$"AttendanceInput[{currentDay - 1}].Comments"]
                    };

                    var existingAttendance = _context.Attendances.FirstOrDefault(a =>
                        a.DoctorId == SelectedDoctor && a.Date.Date == attendanceInputForDay.Date.Date);

                    if (existingAttendance != null && existingAttendance.Published != true)
                    { //existing attendances were being removed here, Adding a constraint to check for published != true was the solution, otherwise the secretary can check/uncheck anything else
                        if (!attendanceInputForDay.Attended.Value)
                        {
                            // Delete the attendance record if the "Attended" checkbox is unchecked
                            _context.Attendances.Remove(existingAttendance);
                        }
                        else
                        {
                            existingAttendance.Attended = true;
                            existingAttendance.Comments = attendanceInputForDay.Comments;
                            existingAttendance.NbHours = attendanceInputForDay.NbHours;
                            existingAttendance.DepId = 1; // new added since depid was causing a conflict on foreign key
                        }
                    }
                    else if (attendanceInputForDay.Attended == true)
                    {
                        // Create a new attendance record only if the Attended checkbox was checked
                        var newAttendance = new Attendance
                        {
                            DoctorId = SelectedDoctor,
                            Date = attendanceInputForDay.Date,
                            Attended = true,
                            Comments = attendanceInputForDay.Comments,
                            NbHours = attendanceInputForDay.NbHours,
                            DepId = 1 // new added since depid was causing a conflict on foreign key
                        };
                        _context.Attendances.Add(newAttendance);
                    }
                }

                await _context.SaveChangesAsync(); // Save changes to the database

                // Set ShowRecords to true to display updated records after form submission
                ShowRecords = true;

                // Refresh the page data to reflect the changes
                await OnGetAsync();

                return Page();
            }

            // Repopulate DoctorItems if there's an error in the model
            Doctor = await _context.Doctors.ToListAsync();
            DoctorItems = Doctor.Select(doctor => new SelectListItem
            {
                Value = doctor.DoctorId.ToString(),
                Text = $"{doctor.Firstname} {doctor.Lastname}"
            }).ToList();

            // If ModelState is invalid, stay on the page and show the validation errors
            return Page();
        }



        private int ConvertToInt32(string value)
        {
            int result;
            int.TryParse(value, out result);
            return result;
        }

        public class AttendanceInputModel
        {
            public int DepId { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime Date { get; set; }
            public int NbHours { get; set; }
            public bool? Attended { get; set; }
            public bool Published { get; set; }
            public string? Comments { get; set; }
        }
    }
}
