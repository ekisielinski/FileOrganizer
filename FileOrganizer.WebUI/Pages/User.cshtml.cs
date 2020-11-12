using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public class UserModel : PageModel
    {
        public AppUserDetails? Details { get; set; }

        //====== actions

        public async Task<IActionResult> OnGet( string userName, [FromServices] IMediator mediator )
        {
            try
            {
                var query = new GetAppUserDetailsQuery( new( userName ) );

                Details = await mediator.Send( query );
            }
            catch
            {
            }

            return Page();
        }
    }
}
