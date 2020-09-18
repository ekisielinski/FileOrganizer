using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.Core.Services;
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

        public IActionResult OnGet( int uploadId, [FromServices] IUploadDetailsReader reader )
        {
            try
            {
                UploadDetails = reader.GetUploadDetails( new UploadId( uploadId ) );
            }
            catch
            {
                // todo: should display some message
                return NotFound();
            }

            return Page();
        }

        //====== public methods

        public string GetThumbFilePath( FileDetails fileDetails )
        {
            return linkGenerator.GetThumbnailPath( fileDetails.DatabaseFiles.Thumbnail );
        }
    }
}
