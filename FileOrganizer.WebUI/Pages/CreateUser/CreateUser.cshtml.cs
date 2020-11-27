using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public class NewUserModel : PageModel
    {
        [BindProperty]
        public CreateUserForm Form { get; set; } = new();

        //--- actions

        public void OnGet()
        {
            // empty
        }

        public async Task<IActionResult> OnPostAsync( [FromServices] IMediator mediator )
        {
            if (ModelState.IsValid == false) return Page();

            try
            {
                var cmd = new CreateAppUserCommand( Form.ToUserName(), Form.ToUserDisplayName(), Form.ToUserPassword() );

                await mediator.Send( cmd );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError( string.Empty, "Failed to create new user. " + ex.Message );

                return Page();
            }

            return RedirectToPage( "/Users" );
        }
    }
}
