using FileOrganizer.Domain;
using FileOrganizer.WebUI.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public class ViewModel : PageModel
    {
        public FileDetails? FileDetails { get; set; }
        public string? FilePath { get; set; }

        public IReadOnlyList<FileLink> Links { get; private set; } = new List<FileLink>();

        //====== actions

        public async Task<IActionResult> OnGet( int fileId,
            [FromServices] IMediator mediator,
            [FromServices] IStaticFilesLinkGenerator linkGenerator)
        {
            FileDetails = await mediator.Send( new GetFileDetailsQuery( new( fileId ) ) );

            if (FileDetails is null) return NotFound();

            Links = await mediator.Send( new GetFileLinksQuery( new( fileId ) ) );

            FilePath = linkGenerator.GetSourceFilePath( FileDetails.DatabaseFiles.Source );

            return Page();
        }
    }
}
