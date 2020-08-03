using FileOrganizer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

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
                UploadInfo[] uploads = Files
                    .Select( x => new UploadInfo( x.OpenReadStream(), new MimeType( x.ContentType ), x.FileName ) )
                    .ToArray();

                // TODO: should we dispose streams after upload?

                UploadResult result = fileUploader.Upload( uploads );

                if (result.FileIds.Count == 1)
                {
                    return RedirectToPage( "View", new { fileId = result.FileIds[0].Value } );
                }

                return RedirectToPage( "UploadResult", new { uploadId = result.Id.Value } );
            }

            Error = "Select at least one file.";

            return Page();
        }
    }
}
