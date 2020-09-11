using FileOrganizer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileOrganizer.WebUI.Pages
{
    public class NewUserModel : PageModel
    {
        [BindProperty, Required]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        public string? UserDisplayName { get; set; }

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public bool IsModerator { get; set; }

        //---

        public void OnGet()
        {

        }

        public IActionResult OnPost( [FromServices] IAppUserCreator creator )
        {
            if (ModelState.IsValid == false) return Page();

            try
            {
                var userName = new UserName( UserName );
                var displayName = new UserDisplayName( UserDisplayName ?? string.Empty );

                var rolesList = new List<UserRole>();
                if (IsModerator) rolesList.Add( UserRole.Moderator );
                var roles = new UserRoles( rolesList );

                var appUser = new AppUser( userName, displayName, roles );

                creator.Create( appUser, new UserPassword( Password ) );
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
