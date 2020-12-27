using Microsoft.AspNetCore.Mvc;

namespace FileOrganizer.WebUI.Pages.Shared.Components
{
    public sealed class PagerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke( PagerModel model )
        {
            return View( model );
        }
    }
}
