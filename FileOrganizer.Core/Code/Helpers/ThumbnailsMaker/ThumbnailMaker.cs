using Microsoft.Extensions.FileProviders;
using System.Drawing;
using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core.Helpers
{
    // todo: needs improvements - should return orginal image size

    public sealed class ThumbnailMaker : IThumbnailMaker
    {
        readonly IImageResizer imageResizer;

        //====== ctors

        public ThumbnailMaker( IImageResizer imageResizer )
        {
            this.imageResizer = Guard.NotNull( imageResizer, nameof( imageResizer ) );
        }

        //====== IThumbnailsMaker

        public ThumbnailMakerResult MakeThumb( IFileInfo fileInfo, Size size )
        {
            using var stream = fileInfo.CreateReadStream();
            using Image srcImage = Image.FromStream( stream );

            Image thumbnail = imageResizer.Resize( srcImage, size, true );

            return new ThumbnailMakerResult( thumbnail, srcImage.Size );
        }
    }
}
