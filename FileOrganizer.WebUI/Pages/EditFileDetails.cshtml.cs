using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using FileOrganizer.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages
{
    public class EditFileDetailsModel : PageModel
    {
        readonly IMediator mediator;

        [BindProperty] public string? Title       { get; set; }
        [BindProperty] public string? Description { get; set; }
        
        [BindProperty, HiddenInput]
        public int FileId { get; set; }

        [BindProperty]
        public PartialDateTimeModel PrimaryDateTime { get; set; }

        //---

        public FileDetails? FileDetails { get; private set; }

        //====== ctors

        public EditFileDetailsModel( IMediator mediator )
            => this.mediator = Guard.NotNull( mediator, nameof( mediator ) );

        //====== actions

        public async Task OnGet( int fileId )
        {
            FileDetails = await mediator.Send( new GetFileDetailsQuery( new FileId( fileId ) ) );
            
            Description = FileDetails.Description.Value;
            Title       = FileDetails.Title.Value;

            PrimaryDateTime = new PartialDateTimeModel( FileDetails.PrimaryDateTime );
        }

        public async Task<IActionResult> OnPost( int fileId )
        {
            if (ModelState.IsValid == false)
            {
                await OnGet( fileId ); // TODO: refactor
                return Page();
            }

            FileTitle       title           = new( Title ?? string.Empty );
            FileDescription description     = new( Description ?? string.Empty );
            PartialDateTime primaryDateTime = PrimaryDateTime.ToPartialDateTime();

            var cmd = new UpdateFileDetailsCommand( new( FileId ), title, description, primaryDateTime );
            await mediator.Send( cmd );

            return RedirectToPage( "View", new { fileId = FileId } );
        }
    }
}
