using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using FileOrganizer.WebUI.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnGet( int uploadId, [FromServices] IMediator mediator )
        {
            try
            {
                var cmd = new GetUploadDetailsQuery( new( uploadId ) );

                UploadDetails = await mediator.Send( cmd );
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
