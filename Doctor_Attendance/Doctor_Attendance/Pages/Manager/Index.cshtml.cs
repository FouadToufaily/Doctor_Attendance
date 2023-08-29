﻿using System;
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

        [BindProperty]
        public List<Attendance> AttendanceRecords { get; set; }
        public async Task OnGetAsync()
        {
            //doctors list
            Doctor = await _context.Doctors.ToListAsync();
            DoctorItems = Doctor.Select(doctor => new SelectListItem
            {
                Value = doctor.DoctorId.ToString(),
                Text = $"{doctor.Firstname} {doctor.Lastname}"
            }).ToList();
            //departments list
            Departments = await _context.Departments.ToListAsync();
            DepItems = Departments.Select(dep => new SelectListItem
            {
                Value = dep.DepId.ToString(),
                Text = dep.DepName.ToString(),
            }).ToList();

            if (SelectedDoctor > 0 && SelectedMonth > 0 && SelectedDep > 0)
            {
                var startDate = new DateTime(DateTime.Now.Year, SelectedMonth, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                AttendanceRecords = await _context.Attendances
                    .Where(a => a.DoctorId == SelectedDoctor && a.Date >= startDate && a.Date <= endDate && a.DepId == SelectedDep)
                    .ToListAsync();
              

                ShowRecords = true;

                // Populate the AttendanceInput list with data from the database, if available
             //  AttendanceInput = AttendanceRecords.Select(a => new SelectListItem
            }
            else
            {
                
                ShowRecords = false;
                AttendanceRecords = new List<Attendance>();
            }
        }
        /*
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var startDate = new DateTime(DateTime.Now.Year, SelectedMonth, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

               

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
        */


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