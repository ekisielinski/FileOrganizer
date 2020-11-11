using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public class MetadataModel : PageModel
    {
        public FileMetadataContainer Metadata { get; private set; } = FileMetadataContainer.Empty;

        public string? Error { get; private set; }

        public bool RenderError            => Error != null;
        public bool RenderMetadata         => Metadata.IsEmpty == false;
        public bool RenderMetadataNotFound => Metadata.IsEmpty && Error is null;

        //====== actions

        public async Task OnGet( int fileId, [FromServices] IMediator mediator )
        {
            try
            {
                Metadata = await mediator.Send( new GetMetadataFromFileContentQuery( new FileId( fileId ) ) );
            }
            catch
            {
                // TODO: logging
                Error = "Something bad happened during extracting file metadata.";
            }
        }
    }
}
