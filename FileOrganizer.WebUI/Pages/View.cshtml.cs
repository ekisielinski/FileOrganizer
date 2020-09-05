using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using FileOrganizer.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class ViewModel : PageModel
    {
        public FileDetails? FileDetails { get; set; }
        public string? FilePath { get; set; }

        //====== actions

        public IActionResult OnGet( int fileId,
            [FromServices] IFileDetailsReader fileDetailsReader,
            [FromServices] IStaticFilesLinkGenerator linkGenerator)
        {
            FileDetails = fileDetailsReader.GetFileDetailsById( new FileId( fileId ) );

            if (FileDetails is null) return NotFound();

            FilePath = linkGenerator.GetDatabaseFilePath( FileDetails.DatabaseFiles.Source , FileDatabaseFolder.SourceFiles );

            return Page();
        }
    }
}
