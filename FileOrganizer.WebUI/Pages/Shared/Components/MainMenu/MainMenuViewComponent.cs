using Microsoft.AspNetCore.Mvc;

namespace FileOrganizer.WebUI.Pages.Shared.Components
{
    public sealed class MainMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
