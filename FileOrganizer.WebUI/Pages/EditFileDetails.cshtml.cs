using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using FileOrganizer.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class EditFileDetailsModel : PageModel
    {
        readonly IFileDetailsReader reader;
        readonly IFileDetailsUpdater updater;

        [BindProperty] public string? Title       { get; set; }
        [BindProperty] public string? Description { get; set; }
        
        [BindProperty, HiddenInput]
        public int FileId { get; set; }

        [BindProperty]
        public PartialDateTimeModel PrimaryDateTime { get; set; }

        //---

        public FileDetails? FileDetails { get; private set; }

        //====== ctors

        public EditFileDetailsModel( IFileDetailsReader reader, IFileDetailsUpdater updater )
        {
            this.reader = reader;
            this.updater = updater;
        }

        //====== actions

        public void OnGet( int fileId )
        {
            FileDetails = reader.GetFileDetailsById( new FileId( fileId ) );
            
            Description = FileDetails.Description.Value;
            Title       = FileDetails.Title.Value;

            PrimaryDateTime = new PartialDateTimeModel( FileDetails.PrimaryDateTime );
        }

        public IActionResult OnPost( int fileId )
        {
            if (ModelState.IsValid == false)
            {
                OnGet( fileId ); // TODO: refactor
                return Page();
            }

            var title = new FileTitle( Title ?? string.Empty );
            var description = new FileDescription( Description ?? string.Empty );
            var primaryDateTime = PrimaryDateTime.ToPartialDateTime();

            updater.UpdateTitle( new FileId( FileId ), title );
            updater.UpdateDescription( new FileId( FileId ), description );
            updater.UpdatePrimaryDateTime( new FileId( FileId ), primaryDateTime );

            return RedirectToPage( "View", new { fileId = FileId } );
        }
    }
}
