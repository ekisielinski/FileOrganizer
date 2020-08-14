using FileOrganizer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class UserModel : PageModel
    {
        public AppUser? AppUser { get; set; }

        //====== actions

        public IActionResult OnGet( string userName, [FromServices] IAppUserFinder userFinder )
        {
            AppUser = userFinder.Find( new UserName( userName ) );

            return Page();
        }
    }
}
