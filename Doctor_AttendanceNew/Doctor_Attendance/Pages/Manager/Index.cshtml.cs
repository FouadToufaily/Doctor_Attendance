﻿using Doctor_Attendance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Doctor_Attendance.Pages.Manager
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> Doctor { get; set; } = default!;
        public IEnumerable<Department> Departments { get; set; } = default!;
        public List<SelectListItem> DoctorItems { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> DepItems { get; set; } = new List<SelectListItem>();
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
        [BindProperty(SupportsGet = true)]
        public int SelectedDep { get; set; }

        public int FacultyId { get; set; }
        public int SectionId { get; set; }
        public Doctor doctor { get; set; }

        [BindProperty]
        public List<Attendance> AttendanceRecords { get; set; }
        public async Task OnGetAsync(int? drId)//from login page 
        {
           
            
            drId = 2; //for testing
            // getting the doctor
            doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == drId);

            //getting faculty that this dr manages
            Faculty faculty =  await _context.Faculties.FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);
            
            //geting departments of this faculty and this section
            var query = _context.Departments
            .Where(d => d.Faculties.Any(f => f.Facultyid == faculty.Facultyid &&
            f.Sections.Any(s => s.DoctorId == drId)));
            //Load list of deps for select
            var departments = await query.ToListAsync();
            DepItems = departments.Select(dep => new SelectListItem
            {
                Value = dep.DepId.ToString(),
                Text = dep.DepName.ToString(),
            }).ToList();

            //Load list of doctors for select
            Doctor = await _context.Doctors.ToListAsync();
            DoctorItems = Doctor.Select(doctor => new SelectListItem
            {
                Value = doctor.DoctorId.ToString(),
                Text = $"{doctor.Firstname} {doctor.Lastname}"
            }).ToList();
            
            //if there is a selected doctor, month and a department
            if (SelectedDoctor > 0 && SelectedMonth > 0 && SelectedDep > 0)
            {
                // setting the start and end date of the selected month
                var startDate = new DateTime(DateTime.Now.Year, SelectedMonth, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                // Getting the specified records
                AttendanceRecords = await _context.Attendances
                    .Where(a => a.DoctorId == SelectedDoctor && a.Published == true && a.Date >= startDate && a.Date <= endDate && a.DepId == SelectedDep)
                    .ToListAsync();

                //setting the flag to display attendance records
                ShowRecords = true;

            }
            else //if ModelState is invalid
            {
                ShowRecords = false;
                //initialize an empty list of attendances
                AttendanceRecords = new List<Attendance>();
            }
          
        }

        public class AttendanceInputModel
        {
            public int DepId { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime Date { get; set; }
            public int NbHours { get; set; }
            public string? Comments { get; set; }
        }
    }
}
