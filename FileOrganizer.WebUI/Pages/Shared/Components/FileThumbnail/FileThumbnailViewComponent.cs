using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using FileOrganizer.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
            // todo: for files w/o thumbnail we should serve special image

            string link = string.Empty;

            if (file.DatabaseFiles.Thumbnail != null)
            {
                link = linkGenerator.GetDatabaseFilePath( file.DatabaseFiles.Thumbnail, FileDatabaseFolder.Thumbnails );
            }

            // TODO: create model

            ViewBag.ThumbLink = link;
            ViewBag.DimensionString = file.ImageDetails.Size is Size size ? $"{size.Width}x{size.Height}" : "unknown";

            return View( file );
        }
    }
}
