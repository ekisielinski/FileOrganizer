using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public sealed class CreateLinkModel : PageModel
    {
        public FileDetails? FileDetails { get; private set; }

        [BindProperty] public CreateLinkForm Form { get; set; } = new();

        //====== actions

        public async Task<IActionResult> OnGet( int? fileId, [FromServices] IMediator mediator )
        {
            if (fileId is null) return BadRequest();

            FileDetails = await mediator.Send( new GetFileDetailsQuery( new FileId( fileId.Value ) ) );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync( int? fileId, [FromServices] IMediator mediator )
        {
            if (ModelState.IsValid == false) return await OnGet( fileId, mediator ); // todo: is is OK?

            var cmd = new AddFileLinkCommand( new ( fileId.Value ), new ( Form.Url ), new ( Form.Title ), new ( Form.Comment ) );
            await mediator.Send( cmd );

            return RedirectToPage( "/View", new { fileId = fileId.Value } );
        }
    }
}
