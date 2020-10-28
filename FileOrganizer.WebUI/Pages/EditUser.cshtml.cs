using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public class EditUserModel : PageModel
    {
        public AppUserDetails? Details { get; set; }

        //---

        [BindProperty] public string? DisplayName { get; set; }
        [BindProperty] public string? Email { get; set; }

        //====== actions

        public async Task OnGet( string userName, [FromServices] IMediator mediator )
        {
            var query = new GetAppUserDetailsQuery( new UserName( userName ) );

            Details = await mediator.Send( query );

            DisplayName = Details.User.DisplayName.Value;
            Email = Details.Email?.Value;
        }

        public async Task<IActionResult> OnPost( string userName, [FromServices] IMediator mediator )
        {
            var emailData = DataUpdateBehavior<EmailAddress>.DeleteValue();

            if (!string.IsNullOrEmpty( Email ))
            {
                emailData = DataUpdateBehavior<EmailAddress>.CreateOrUpdateValue( new EmailAddress( Email ) );
            }

            var cmd = new UpdateAppUserDetailsCommand(
                new UserName( userName ),
                new UserDisplayName( DisplayName ?? string.Empty ),
                emailData );

            await mediator.Send( cmd );

            return RedirectToPage( "User", new { userName = userName } );
        }
    }
}
