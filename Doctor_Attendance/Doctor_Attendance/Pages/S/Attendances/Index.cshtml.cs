using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Attendances
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IList<Attendence> Attendence { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Attendences != null)
            {
                Attendence = await _context.Attendences
                .Include(a => a.Dep)
                .Include(a => a.Doctor).ToListAsync();
            }
        }
    }
}
