using FileOrganizer.CommonUtils;
using FileOrganizer.WebUI.Areas.Auth.Models;
using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Areas.Auth.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        readonly IAuthService authService;

        //====== ctors

        public LoginModel( IAuthService authService )
        {
            this.authService = Guard.NotNull( authService, nameof( authService ) );
        }

        //====== public properties

        [BindProperty] public LoginForm Form { get; set; } = new();

        public string? Error { get; private set; }

        //====== actions

        public IActionResult OnGet()
        {
            if (authService.CurrentUser is null) return Page();

            return RedirectToPage( "Index" );
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false) return Page();

            if (authService.Login( new( Form.UserName! ), Form.Password! ))
            {
                return RedirectToPage( "Index" );
            }

            Error = "Invalid credentials!";

            return Page();
        }
    }
}
