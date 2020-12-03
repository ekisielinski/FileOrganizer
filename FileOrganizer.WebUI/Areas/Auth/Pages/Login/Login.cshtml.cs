using FileOrganizer.CommonUtils;
using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Areas.Auth.Pages.Login
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

        //====== actions

        public IActionResult OnGet()
        {
            if (authService.CurrentUser is null) return Page();

            return RedirectToPage( "/Index" );
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false) return Page();

            bool loggedIn = await authService.LoginAsync( new( Form.UserName! ), Form.Password! );

            if (loggedIn) return RedirectToPage( "/Index" );

            ModelState.AddModelError( string.Empty, "Invalid credentials!" );

            return Page();
        }
    }
}
