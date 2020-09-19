using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class EditFileDetailsModel : PageModel
    {
        [BindProperty] public string? Title       { get; set; }
        [BindProperty] public string? Description { get; set; }
        
        [BindProperty, HiddenInput]
        public int FileId { get; set; }

        //---

        public FileDetails? FileDetails { get; private set; }

        //====== actions

        public void OnGet( int fileId, [FromServices] IFileDetailsReader reader )
        {
            FileDetails = reader.GetFileDetailsById( new FileId( fileId ) );
            
            Description = FileDetails.Description.Value;
            Title       = FileDetails.Title.Value;
        }

        public IActionResult OnPost( [FromServices] IFileDetailsUpdater updater )
        {
            var title = new FileTitle( Title ?? string.Empty );
            var description = new FileDescription( Description ?? string.Empty );

            updater.UpdateTitle( new FileId( FileId ), title );
            updater.UpdateDescription( new FileId( FileId ), description );

            return RedirectToPage( "View", new { fileId = FileId } );
        }
    }
}
