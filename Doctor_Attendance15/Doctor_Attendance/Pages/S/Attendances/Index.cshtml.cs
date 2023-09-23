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
        public string? EmpDep { get; set; }

        public string? DoctorDep { get; set; }
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
                    .Select(u => u.Dep.DepName)
                    .FirstOrDefaultAsync();

                // getting attendance with dep of the emp
                if (EmpDep != null)
                {
                    Attendances = await _context.Attendances
                                                .Include(a => a.Dep)
                                                .Include(a => a.Doctor)
                                                .Where(a => a.Dep.DepName.Equals(EmpDep))
                                                .ToListAsync();
                }
                if (!EmpId.HasValue)//no docId and no empId the it is an admin
                {
                    if (RoleName.Equals("Admin"))
                    {
                        Attendances = await _context.Attendances
                                            .Include(a => a.Dep)
                                            .Include(a => a.Doctor).ToListAsync();
                    }

                }
            }

            else if (DoctorDep != null)
            {
                if (RoleName.Equals("HOD"))
                {
                    Attendances = await _context.Attendances.Include(a => a.Dep)
                        .Include(a => a.Doctor)
                        .Include(a => a.Dep)
                        .Where(a => a.Dep.DepName.Equals(DoctorDep) && a.Published == true)
                        .ToListAsync();
                }
                /*
                else
                {
                    Attendances = await _context.Attendances.Include(a => a.Dep)
                        .Include(a => a.Doctor).ToListAsync();
                }
                */

            }
            else
            {
                Attendances = await _context.Attendances
                                            .Include(a => a.Dep)
                                            .Include(a => a.Doctor).ToListAsync();
            }

            if (RoleName.Equals("Admin") || RoleName.Equals("HOS") || RoleName.Equals("HOF"))
            {                //Attendances = _context.SearchAttendance(SearchString).ToList<Attendance>();
                if (RoleName.Equals("HOS") || RoleName.Equals("HOF"))
                {
                    // calling Search function by search criteria which is binded to the form control textbox
                    //  var Attendances1 = _context.SearchAttendance(SearchString);
                    Attendances = _context.SearchAttendance(SearchString).ToList<Attendance>();
                    // Retrieve only published attendances
                    //  Attendances = Attendances1.Where(a => a.Published == true).ToList<Attendance>();
                }
                else //if it's an admin
                {
                    //Retrieve all attendances
                    Attendances = _context.SearchAttendance(SearchString).ToList<Attendance>();  
                }
                
            }
            else // searchdoctor should return only doctors of same dep and HODs and hos and hof 
            {
                if (EmpDep != null)//if it's a sec
                {
                    var Attendances1 = _context.SearchAttendance(SearchString);
                    Attendances = Attendances1.Where(e => e.Dep != null && e.Dep.DepName == EmpDep).ToList<Attendance>();
                }
                else// if HOD 
                {
                    var Attendances1 = _context.SearchAttendance(SearchString);
                    Attendances = Attendances1.Where(e => e.Dep != null && e.Dep.DepName == DoctorDep && e.Published == true).ToList<Attendance>();
                }
                //Attendances = _context.SearchAttendance(SearchString).ToList<Attendance>();
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

