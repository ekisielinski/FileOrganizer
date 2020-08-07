using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using FileOrganizer.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FileOrganizer.WebUI.Pages
{
    public class UploadResultModel : PageModel
    {
        readonly IStaticFilesLinkGenerator linkGenerator;

        public IReadOnlyList<FileDetails>? Files { get; private set; }

        //====== ctors

        public UploadResultModel( IStaticFilesLinkGenerator linkGenerator )
        {
            this.linkGenerator = Guard.NotNull( linkGenerator, nameof( linkGenerator ) );
        }

        //====== actions

        public IActionResult OnGet( int uploadId, [FromServices] IFileDetailsReader reader )
        {
            Files = reader.GetFileDetailsByUploadId( new UploadId( uploadId ) );

            if (Files.Count == 0) return NotFound();

            return Page();
        }

        //====== public methods

        public string GetThumbFilePath( FileDetails fileDetails )
        {
            return linkGenerator.GetDatabaseFilePath( fileDetails.DatabaseFiles.Thumbnail, FileDatabaseFolder.Thumbs );
        }
    }
}
