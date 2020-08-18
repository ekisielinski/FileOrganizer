using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class EditFileDetailsModel : PageModel
    {
        [BindProperty] public string Title       { get; set; } = string.Empty;
        [BindProperty] public string Description { get; set; } = string.Empty;
        
        [BindProperty, HiddenInput]
        public int FileId { get; set; }

        //---

        public FileDetails FileDetails { get; set; }

        //====== actions

        public void OnGet( int fileId, [FromServices] IFileDetailsReader reader )
        {
            FileDetails = reader.GetFileDetailsById( new FileId( fileId ) );
            
            Description = FileDetails.Description.Value;
            Title = FileDetails.Title.Value;
        }

        public IActionResult OnPost( [FromServices] IFileDetailsUpdater updater )
        {
            updater.UpdateTitle( new FileId( FileId ), new FileTitle( Title ) );
            updater.UpdateDescription( new FileId( FileId ), new FileDescription( Description ) );

            return RedirectToPage( "View", new { fileId = FileId } );
        }
    }
}
