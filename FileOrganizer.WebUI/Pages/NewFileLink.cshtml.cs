using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public class NewFileLinkModel : PageModel
    {
        public FileDetails? FileDetails { get; private set; }

        // todo: fluent validation
        [BindProperty] public string LinkUrl     { get; set; }
        [BindProperty] public string LinkTitle   { get; set; }
        [BindProperty] public string LinkComment { get; set; }

        public async Task<IActionResult> OnGet( int? fileId, [FromServices] IMediator mediator )
        {
            if (fileId is null) return BadRequest();

            FileDetails = await mediator.Send( new GetFileDetailsQuery( new FileId( fileId.Value ) ) );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync( int? fileId, [FromServices] IMediator mediator )
        {
            await mediator.Send( new AddFileLinkCommand(
                new FileId( fileId.Value ),
                new LinkUrl( LinkUrl ),
                new LinkTitle( LinkTitle ),
                new LinkComment( LinkComment )
                ) );

            return RedirectToPage( "View", new { fileId = fileId.Value } );
        }
    }
}
