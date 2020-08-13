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
            FileDetails? details = fileDetailsReader.GetFileDetailsById( new FileId( fileId ) );

            if (details is null) return NotFound();

            FilePath = linkGenerator.GetDatabaseFilePath( details.DatabaseFiles.Source , FileDatabaseFolder.Files );

            return Page();
        }
    }
}
