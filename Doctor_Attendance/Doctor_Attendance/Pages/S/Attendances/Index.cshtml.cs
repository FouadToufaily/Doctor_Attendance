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

        public IEnumerable<Attendance> Attendances { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; } = default!;
        [BindProperty]
        public string s { get; set; }
        [BindProperty]
        public string s1 { get; set; }

        public async Task OnGetAsync()
        {
            Attendances = await _context.Attendances.Include(a => a.Dep)
                     .Include(a => a.Doctor).ToListAsync();
            Attendances = _context.SearchAttendance(SearchString);
        }
        public IActionResult OnPostPublish(int attendanceToPublishId)
        {
            //s1 = "in publish "+ attendanceToPublishId;

            var att = _context.Attendances.FirstOrDefault(a => a.AttId == attendanceToPublishId);
            if (att is not null)
            att.Published = 1;
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }

    }
}

