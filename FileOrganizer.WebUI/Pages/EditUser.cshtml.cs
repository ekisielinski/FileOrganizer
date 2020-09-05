using FileOrganizer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class EditUserModel : PageModel
    {
        public AppUserDetails? Details { get; set; }

        //---

        [BindProperty]
        public string? DisplayName { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        //====== actions

        public void OnGet( string userName, [FromServices] IAppUserReader reader )
        {
            Details = reader.GetUserDetails( new UserName( userName ) );

            DisplayName = Details.User.DisplayName.Value;
            Email = Details.Email?.Value;
        }

        public IActionResult OnPost( string userName, [FromServices] IAppUserUpdater updater )
        {
            var un = new UserName( userName );

            updater.SetDisplayName( un, new UserDisplayName( DisplayName ?? string.Empty ) );

            EmailAddress? email = null;

            if (!(Email is null))
            {
                email = new EmailAddress( Email );
            }

            updater.SetEmail( un, email );

            return RedirectToPage( "User", new { userName = userName } );
        }
    }
}
