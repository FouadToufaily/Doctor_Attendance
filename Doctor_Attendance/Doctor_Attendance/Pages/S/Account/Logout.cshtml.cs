using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Doctor_Attendance.Pages.S.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.SetString("UserStatus", "");
        }
    }
}
