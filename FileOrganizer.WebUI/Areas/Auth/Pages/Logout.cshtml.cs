using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Areas.Auth.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            return BadRequest();
        }

        public IActionResult OnPost([FromServices] IAuthService authService )
        {
            if (authService.CurrentUser is null) return RedirectToPage( "Login" );

            authService.Logout();

            return Page();
        }
    }
}
