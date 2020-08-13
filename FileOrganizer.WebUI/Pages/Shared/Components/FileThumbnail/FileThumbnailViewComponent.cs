using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using FileOrganizer.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileOrganizer.WebUI.Pages.Shared.Components
{
    public sealed class FileThumbnailViewComponent : ViewComponent
    {
        readonly IStaticFilesLinkGenerator linkGenerator;

        //====== ctors

        public FileThumbnailViewComponent( IStaticFilesLinkGenerator linkGenerator )
        {
            this.linkGenerator = Guard.NotNull( linkGenerator, nameof( linkGenerator ) );
        }

        //====== view component

        public IViewComponentResult Invoke( FileDetails file )
        {
            string link = linkGenerator.GetDatabaseFilePath( file.DatabaseFiles.Thumbnail, FileDatabaseFolder.Thumbs );

            ViewBag.ThumbLink = link;

            return View( file );
        }
    }
}
