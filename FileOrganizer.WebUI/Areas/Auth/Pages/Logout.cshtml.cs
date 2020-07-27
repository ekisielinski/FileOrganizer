using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Areas.Auth.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnPost([FromServices]  IAuthService authService )
        {
            authService.Logout();
        }
    }
}
