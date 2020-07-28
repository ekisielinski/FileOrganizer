using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FileOrganizer.WebUI.Areas.Auth.Pages
{
    // TODO: add return Url

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        readonly IAuthService authService;

        //====== ctors

        public LoginModel( IAuthService authService ) => this.authService = authService;

        //====== public properties

        [BindProperty]
        [Required( ErrorMessage = "User name is required." )]
        public string? UserName { get; set; }

        [BindProperty]
        [Required( ErrorMessage = "Password is required." )]
        public string? Password { get; set; }

        //---

        public string? Error { get; private set; }

        //--- actions

        public IActionResult OnGet()
        {
            if (authService.CurrentUser != null) return RedirectToPage( "Index" );

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (authService.Login( new UserName( UserName! ), Password! ))
            {
                return RedirectToPage( "Index" );
            }

            Error = "Invalid credentials!";

            return Page();
        }
    }
}
