using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Doctor_Attendance.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential credential { get; set; }  
        public void OnGet()
        {
        }

        public class Credential
        {
            [Required]
            [EmailAddress]
            public string Username { get; set; }    

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }    
        }
    }
}
