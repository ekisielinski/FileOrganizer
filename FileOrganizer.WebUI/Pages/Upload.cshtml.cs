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

        public IActionResult OnPost([FromServices] IFileUploader fileUploader )
        {
            if (Files?.Count > 0)
            {
                FileId? lastFileId = null;

                foreach( IFormFile file in Files)
                {
                    using var stream = file.OpenReadStream(); // TODO: is dispose neccessary?
                    // TODO: file.Length, avoid big files

                    lastFileId = fileUploader.Upload( stream, new MimeType( file.ContentType ), file.FileName );
                }

                return RedirectToPage( "View", new { fileId = lastFileId!.Value } );
            }

            Error = "Select at least one file.";

            return Page();
        }
    }
}
