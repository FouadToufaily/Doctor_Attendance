using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using proj439.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace proj439.Pages.ViewByDate
{
    [BindProperties]
    public class AddAttendanceModel : PageModel
    {

        private readonly DoctorAttendanceContext dbContext;
        public AddAttendanceModel(DoctorAttendanceContext dbContext)
        {
            this.dbContext = dbContext;
            AttendenceRecords = new List<Attendence>();
        }

        [BindProperty]
        public List<Attendence> AttendenceRecords { get; set; }

        public IList<Doctor> Doctor { get; set; } = default!;
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)] // Add this line
        public DateTime datev { get; set; }

        public async Task OnGetAsync()
        {
            if (dbContext.Doctors != null)
            {
                Doctor = await dbContext.Doctors
                .Include(d => d.Category).ToListAsync();
            }
            foreach (var doctor in Doctor)
            {
                Attendence attendence = new Attendence();
                attendence.DoctorId = doctor.DoctorId; // Assign the correct DoctorId
                AttendenceRecords.Add(attendence);
            }

        }

        public IActionResult OnPost()
        {
            
            var checkedRecords = AttendenceRecords.Where(r => r.Attend==true).ToList();
            // Get the last AttId from the table
            int? lastAttId = dbContext.Attendences.Select(r => (int?)r.AttId).Max();

            // Assign incremented AttId values
            int attId = lastAttId.HasValue ? lastAttId.Value + 1 : 1;
            foreach (var record in checkedRecords)
            {
                record.AttId = attId;
                attId++; // Increment the AttId value
            }
            dbContext.Attendences.AddRange(checkedRecords);
            dbContext.SaveChanges();

            return RedirectToPage();
        }
    }
}
