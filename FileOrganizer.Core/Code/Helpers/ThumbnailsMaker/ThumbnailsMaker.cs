using Microsoft.Extensions.FileProviders;
using System.Drawing;
using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core.Helpers
{
    // todo: needs improvements - should return orginal image size

    public sealed class ThumbnailsMaker : IThumbnailsMaker
    {
        readonly IImageResizer imageResizer;

        //====== ctors

        public ThumbnailsMaker( IImageResizer imageResizer )
        {
            this.imageResizer = Guard.NotNull( imageResizer, nameof( ImageResizer ) );
        }

        //====== IThumbnailsMaker

        public Image MakeThumb( IFileInfo fileInfo, Size size )
        {
            using var stream = fileInfo.CreateReadStream();
            using Image srcImage = Image.FromStream( stream );

            return imageResizer.Resize( srcImage, size, true );
        }
    }
}
