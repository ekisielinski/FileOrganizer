using FileOrganizer.Core.Services;
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

        public void OnPost([FromServices] IFileUploader fileUploader )
        {
            if (Files?.Count > 0)
            {
                foreach( IFormFile file in Files)
                {
                    using var stream = file.OpenReadStream(); // TODO: is dispose neccessary?
                    // TODO: file.Length, avoid big files

                    fileUploader.Upload( stream, new MimeType( file.ContentType ), file.FileName );
                }

                return;
            }

            Error = "Select at least one file.";
        }
    }
}
