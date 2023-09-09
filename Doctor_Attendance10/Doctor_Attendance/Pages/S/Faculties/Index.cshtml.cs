using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Doctor_Attendance.Models;
using Doctor_Attendance.Services;

namespace Doctor_Attendance.Pages.S.Faculties
{
    public class IndexModel : PageModel
    {
        private readonly Doctor_Attendance.Services.AppDBContext _context;

        public IndexModel(Doctor_Attendance.Services.AppDBContext context)
        {
            _context = context;
        }

        public IList<Faculty> Faculty { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Faculties != null)
            {
                Faculty = await _context.Faculties
                .Include(f => f.Doctor).Include(f => f.Sections).ToListAsync();
            }
        }
    }
}
