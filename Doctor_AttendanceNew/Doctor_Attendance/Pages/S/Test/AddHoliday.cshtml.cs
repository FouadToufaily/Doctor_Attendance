using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Doctor_Attendance.Pages.S.Test
{
    public class AddHolidayModel : PageModel
    {
        private readonly AppDBContext _context;
        public AddHolidayModel(AppDBContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public HolidayInputModel Input { get; set; }

        public List<Holiday> Holidays { get; set; }

        public string ValidStr { get; set; }
        public async Task OnGetAsync()
        {
            //Setting the date initially
            Input.Date = DateTime.Now;

            //Setting the Holidays List
            Holidays = await _context.Holidays.ToListAsync();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            //Reset validation string
             ValidStr = "";
            if (ModelState.IsValid)
            {
                //Check if this date already exists in the db 
                var dateExists = _context.Holidays.Any(h => h.Date == Input.Date);
                if(!dateExists)
                {
                    //gettring the last ID in db
                    var lastID = _context.Holidays.Select(h => h.ID).Max();
                    int currentID = lastID + 1;
                    var newHoliday = new Holiday
                    {
                        ID = currentID,
                        Date = (DateTime)Input.Date,
                        Name = Input.Reason
                    };
                    _context.Holidays.Add(newHoliday);
                    await _context.SaveChangesAsync();
                    // Refresh the page data to reflect the changes
                    await OnGetAsync();
                }
                else
                {
                    ValidStr = "A holiday already exists at this date.";
                    Holidays = await _context.Holidays.ToListAsync();
                }
                
                return Page();


            }
            /*
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
                Console.ReadLine();
            }
            */
         
            ValidStr = "Please select a date and type a reason for the holiday.";
            //Setting the Holidays List
            Holidays = await _context.Holidays.ToListAsync();
            return Page();
        }


        public class HolidayInputModel
        {
            [Required]
            [Display(Name = "Date")]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
            [Required]
            [Display(Name = "Reason")]
            public string Reason { get; set; }
        }
    }
}
