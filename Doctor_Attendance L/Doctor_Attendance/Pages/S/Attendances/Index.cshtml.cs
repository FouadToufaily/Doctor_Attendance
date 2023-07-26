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
        [BindProperty ]
        public string s { get; set; }
        [BindProperty]
        public string s1 { get; set; }

        public int attendanceToPublishId { get; set; } 
        public async Task OnGetAsync()
        {   
            Attendances = await _context.Attendances.Include(a => a.Dep)
                     .Include(a => a.Doctor).ToListAsync();

            if (String.IsNullOrEmpty(SearchString))
                return;
          
            
            var attendances = from a in _context.Attendances
                              select a;

            //get doctors whose names contains searched string
            var doctors = _context.Doctors.AsEnumerable().Where(s => s.Fullname.Contains(SearchString));

            IQueryable<Attendance> attendances1 = from a in _context.Attendances
                                                  where a.DoctorId == doctors.ToList().ElementAt(0).DoctorId
                                                  select a;
          
              foreach (var d in doctors)
              {
                  // Get attendances for each matched drId 

                  IQueryable<Attendance> attendances2 = from a in _context.Attendances
                                                        where a.DoctorId == d.DoctorId
                                                        select a;
                //Add attendances to our list
                attendances1 = attendances1.Union(attendances2);

              }


            Attendances = await attendances1.AsQueryable().Include(a => a.Dep)
        .Include(a => a.Doctor).ToListAsync();
        }
             public async Task<IActionResult> OnPostPublish( int attendanceToPublishId)
        {
            //s1 = "in publish "+ attendanceToPublishId;
            
                var att = _context.Attendances.FirstOrDefault(a => a.AttId == attendanceToPublishId);
               
                att.Published = 1;
                _context.SaveChanges();
                return RedirectToPage("./Index");
           

            }

    }
}

