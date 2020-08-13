﻿using FileOrganizer.Core.Services;
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

        [BindProperty]
        public string? Description { get; set; }

        //---

        public string? Error { get; private set; }

        //====== actions

        public void OnGet()
        {

        }

        public IActionResult OnPost( [FromServices] IFileUploader fileUploader, [FromServices] IFileDetailsReader reader )
        {
            if (Files?.Count > 0)
            {
                UploadInfo[] uploads = Files
                    .Select( x => new UploadInfo( x.OpenReadStream(), new MimeType( x.ContentType ), x.FileName ) )
                    .ToArray();

                // TODO: should we dispose streams after upload?

                UploadId uploadId = fileUploader.Upload( uploads, Description );

                var result = reader.GetUploadDetails( uploadId );

                if (result.Files.Count == 1)
                {
                    return RedirectToPage( "View", new { fileId = result.Files[0].FileId.Value } );
                }

                return RedirectToPage( "UploadResult", new { uploadId = result.Id.Value } );
            }

            Error = "Select at least one file.";

            return Page();
        }
    }
}
