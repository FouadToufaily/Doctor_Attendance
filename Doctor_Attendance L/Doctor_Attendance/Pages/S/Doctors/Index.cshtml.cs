using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Doctors
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctor { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Doctors != null)
            {
                Doctor = await _context.Doctors
                .Include(d => d.Category).ToListAsync();
            }
            if (String.IsNullOrEmpty(SearchString))
                return;

            //get doctors whose names contains searched string
             var doctors = _context.Doctors.AsEnumerable().Where(s => s.Fullname.ToLower().Contains(SearchString.ToLower())).ToList();
            /*
            IQueryable<Doctor> doctors = (from d in _context.Doctors
                                         where d.Fullname.Contains(SearchString)
                                         select d);
            */
          //  Doctor = Doctor.AsQueryable().AsEnumerable();
           Doctor =  await doctors.AsQueryable().Include(a => a.Category)
       .Include(a => a.Attendances).ToListAsync();
        }
    }
}
