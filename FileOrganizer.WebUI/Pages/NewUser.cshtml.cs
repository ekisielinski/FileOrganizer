using FileOrganizer.Core;
using FileOrganizer.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace FileOrganizer.WebUI.Pages
{
    public class NewUserModel : PageModel
    {
        [BindProperty]
        public CreateNewUserRequest Model { get; set; } = new CreateNewUserRequest();

        //--- actions

        public void OnGet()
        {

        }

        public IActionResult OnPost( [FromServices] IAppUserCreator creator )
        {
            if (ModelState.IsValid == false) return Page();

            try
            {
                creator.Create( Model.ToAppUser(), Model.ToUserPassword() );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError( "", "Failed to create new user. " + ex.Message );

                return Page();
            }

            return RedirectToPage( "Users" );
        }
    }
}
