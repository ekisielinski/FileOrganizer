using FileOrganizer.Core.Services;
using FileOrganizer.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class ViewModel : PageModel
    {
        public FileDetails? FileDetails { get; set; }
        public string? FilePath { get; set; }

        //====== actions

        public IActionResult OnGet( int fileId, [FromServices] IFileDetailsReader reader  )
        {
            FileDetails? details = reader.GetFileDetailsById( new FileId( fileId ) );

            if (details is null) return NotFound();

            FilePath = Url.ActionLink(
                action: nameof( StaticFilesController.File ),
                controller: "StaticFiles", // TODO: controller name utils
                values: new { fileName = details.FileNameInDatabase } );

            return Page();
        }
    }
}
