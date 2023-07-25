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
        public List<int> drsId { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; } = default!;

        [BindProperty]
        public string s { get; set; } = default!;

        [BindProperty]

        public string s1 { get; set; } = default!;


        public async Task OnGetAsync()
        {   /*
            if (SearchString == null)
                return;
            
            
            if (SearchString.Equals(String.Empty))
            {
                s = " search is empty ";

            }
            */
            Attendances = await _context.Attendances.Include(a => a.Dep)
                     .Include(a => a.Doctor).ToListAsync();
            if (String.IsNullOrEmpty(SearchString))
            {
                s = " search is empty ";

                return;
            }

            var attendances = from a in _context.Attendances
                              select a;

            //get doctors whose names contains searched string
            var doctors = _context.Doctors.AsEnumerable().Where(s => s.Fullname.Contains(SearchString));

           // List<Attendance> attendances1 = new List<Attendance>();
            //IQueryable<Attendance> attendances1 ;
            foreach (var d in doctors)
            {
                s += d.Fullname + " ";

                // Get attendances with drId 
                var atts = GetAttendanceswithDrID(d.DoctorId);
                foreach(var att in atts)
                   attendances.ToList().Add(att);
            }
            foreach (var a in attendances)
            {
                s1 += a.AttId + " ";

            }
            Attendances = await attendances.AsQueryable().Include(a => a.Dep)
                    .Include(a => a.Doctor).ToListAsync();
        }

        public IEnumerable<Attendance> GetAttendanceswithDrID(int id)
        {
            List<Attendance> attendances = new List<Attendance>();
            attendances =_context.Attendances.Where(e => e.DoctorId == id).ToList(); // hayde k2nna for each, mannik mettara t3mleeha
            return attendances;
        }
        public IEnumerable<Attendance> GetAttendanceswithAttID(int id)
        {
            var attendences = _context.Attendances.Where(m => m.AttId == id).ToList();
            return attendences; 
        }

    }
    }
        

          /*
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Attendances == null)
            {
                return NotFound();
            }

            
            // var Attendances1 = from m in _context.Attendances
               //               select m;

            var Doctors = from d in _context.Doctors
                              select d;
            
            int i;

            if (!String.IsNullOrEmpty(searchString))
            {
                
                //Get dr with searched names
                 Doctors = Doctors.Where(d => d.Fullname.Contains(searchString));
                if (Doctors != null)
                {
                    foreach(var d in Doctors)
                        Attendances = Attendances.Where(s => s.DoctorId == d.DoctorId).ToList();
                }
                // Get attendances with drID of searched drs
                

            }
            

            return Index(await Attendances.ToListAsync());
        }
        */
