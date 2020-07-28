using FileOrganizer.CommonUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Linq;

namespace FileOrganizer.WebUI.Controllers
{
    [Route( "static/{action}/{fileName}" )]
    [Authorize]
    public class StaticFilesController : Controller
    {
        readonly IWebHostEnvironment webHostEnvironment;

        //====== ctors

        public StaticFilesController( IWebHostEnvironment webHostEnvironment )
        {
            this.webHostEnvironment = Guard.NotNull( webHostEnvironment, nameof( webHostEnvironment ) );
        }

        //====== private properties

        private IDirectoryContents FilesDir  => webHostEnvironment.ContentRootFileProvider.GetDirectoryContents( "/Database/Files" );
        private IDirectoryContents ThumbsDir => webHostEnvironment.ContentRootFileProvider.GetDirectoryContents( "/Database/Thumbs" );

        //====== actions

        public IActionResult File( string fileName )
        {
            IFileInfo file = FilesDir.FirstOrDefault( x => x.Name == fileName );

            return FileInfoToActionResult( file );
        }

        public IActionResult Thumb( string fileName )
        {
            IFileInfo file = ThumbsDir.FirstOrDefault( x => x.Name == fileName );

            return FileInfoToActionResult( file );
        }

        //====== private methods

        private IActionResult FileInfoToActionResult( IFileInfo file )
        {
            if (file is null) return NotFound();

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
