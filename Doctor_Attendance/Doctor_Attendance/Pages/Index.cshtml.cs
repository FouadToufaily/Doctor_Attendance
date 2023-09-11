using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Doctor_Attendance.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDBContext _context;
        public User User { get; set; }
        public IndexModel(ILogger<IndexModel> logger, AppDBContext context )
        {
            _logger = logger;
            _context = context;
        }
        
        public void OnGet()
        {
            string username = HttpContext.Session.GetString("UserStatus");
            Console.WriteLine(username);
            if(!String.IsNullOrEmpty(username))
            {
                //get the user if logged in
                User User = _context.Users.FirstOrDefault(u => u.Username == username);
                // set the roleId visible to the layout page and all other pages
                HttpContext.Session.SetString("RoleId", User.RoleId.ToString());
            }
            
        }
    }
}