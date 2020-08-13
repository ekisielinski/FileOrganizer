using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using FileOrganizer.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class UploadResultModel : PageModel
    {
        readonly IStaticFilesLinkGenerator linkGenerator;

        public UploadDetails UploadDetails { get; private set; }

        //====== ctors

        public UploadResultModel( IStaticFilesLinkGenerator linkGenerator )
        {
            this.linkGenerator = Guard.NotNull( linkGenerator, nameof( linkGenerator ) );
        }

        //====== actions

        public IActionResult OnGet( int uploadId, [FromServices] IFileDetailsReader reader )
        {
            UploadDetails = reader.GetUploadDetails( new UploadId( uploadId ) );

            if (UploadDetails is null) return NotFound();

            return Page();
        }

        //====== public methods

        public string GetThumbFilePath( FileDetails fileDetails )
        {
            return linkGenerator.GetDatabaseFilePath( fileDetails.DatabaseFiles.Thumbnail, FileDatabaseFolder.Thumbs );
        }
    }
}
