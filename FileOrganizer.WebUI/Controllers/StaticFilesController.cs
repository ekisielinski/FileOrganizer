using FileOrganizer.CommonUtils;
using FileOrganizer.Services.FileDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace FileOrganizer.WebUI.Controllers
{
    [Route( "static/{action}/{fileName}" )]
    [Authorize]
    public class StaticFilesController : Controller
    {
        readonly IFileDatabaseReader reader;

        //====== ctors

        public StaticFilesController( IFileDatabaseReader reader )
        {
            this.reader = Guard.NotNull( reader, nameof( reader ) );
        }

        //====== actions

        public IActionResult File( string fileName )
        {
            IFileInfo file = reader.GetStorageReader( FileDatabaseFolder.Files )
                                   .Get( new FileName( fileName ));

            return FileInfoToActionResult( file );
        }

        public IActionResult Thumb( string fileName )
        {
            IFileInfo file = reader.GetStorageReader( FileDatabaseFolder.Thumbs )
                                   .Get( new FileName( fileName ));

            return FileInfoToActionResult( file );
        }

        //====== private methods

        private IActionResult FileInfoToActionResult( IFileInfo file )
        {
            if (!file.Exists) return NotFound();

            // TODO: should return file if mime type is supported by the browser, othwerise should return fixed icon(?)

            var provider = new FileExtensionContentTypeProvider();

            if (provider.TryGetContentType( file.Name, out string contentType ))
            {
                return PhysicalFile( file.PhysicalPath, contentType );
            }

            return NotFound();
        }
    }
}
