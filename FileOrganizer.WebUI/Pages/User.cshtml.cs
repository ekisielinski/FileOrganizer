using FileOrganizer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class UserModel : PageModel
    {
        public AppUserDetails? Details { get; set; }

        //====== actions

        public IActionResult OnGet( string userName, [FromServices] IAppUserReader reader )
        {
            try
            {
                Details = reader.GetUserDetails( new UserName( userName ) );
            }
            catch
            {
            }

            return Page();
        }
    }
}
