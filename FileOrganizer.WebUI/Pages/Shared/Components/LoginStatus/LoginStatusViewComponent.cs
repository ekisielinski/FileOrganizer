using FileOrganizer.CommonUtils;
using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FileOrganizer.WebUI.Pages.Shared.Components
{
    public sealed class LoginStatusViewComponent : ViewComponent
    {
        readonly IAuthUserAccessor accessor;

        //====== ctors

        public LoginStatusViewComponent( IAuthUserAccessor accessor )
        {
            this.accessor = Guard.NotNull( accessor, nameof( accessor ) );
        }

        //====== view component

        public IViewComponentResult Invoke()
        {
            AuthUser? model = accessor.CurrentUser;

            return View( model );
        }
    }
}
