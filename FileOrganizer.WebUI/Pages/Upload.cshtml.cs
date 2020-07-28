using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace FileOrganizer.WebUI.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFileCollection? Files { get; set; }

        //---

        public string? Error { get; private set; }

        //====== actions

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (Files?.Count > 0)
            {
                return;
            }

            Error = "Select at least one file.";
        }
    }
}
