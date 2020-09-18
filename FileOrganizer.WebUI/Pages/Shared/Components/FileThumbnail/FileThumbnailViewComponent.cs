using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Services;
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

            string thumbLink = string.Empty;

            if (file.DatabaseFiles.Thumbnail != null)
            {
                thumbLink = linkGenerator.GetThumbnailPath( file.DatabaseFiles.Thumbnail );
            }

            // TODO: create model

            ViewBag.ThumbLink = thumbLink;
            ViewBag.DimensionString = file.ImageDetails.Size is Size size ? $"{size.Width}x{size.Height}" : "unknown";

            return View( file );
        }
    }
}
