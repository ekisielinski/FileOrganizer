using FileOrganizer.Domain;
using FileOrganizer.WebUI.Models;
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
        public CreateNewUserRequest Model { get; set; } = new();

        //--- actions

        public void OnGet()
        {
            // empty
        }

        public async Task<IActionResult> OnPost( [FromServices] IMediator mediator )
        {
            if (ModelState.IsValid == false) return Page();

            try
            {
                var command = new CreateAppUserCommand( Model.ToUserName(), Model.ToUserDisplayName(), Model.ToUserPassword() );

                await mediator.Send( command );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError( string.Empty, "Failed to create new user. " + ex.Message );

                return Page();
            }

            return RedirectToPage( "Users" );
        }
    }
}
