using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Areas.Auth.Pages.Logout
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync( [FromServices] IAuthService authService )
        {
            if (authService.CurrentUser is null) return RedirectToPage( "Login" );

            await authService.LogoutAsync();

            return Page();
        }
    }
}
