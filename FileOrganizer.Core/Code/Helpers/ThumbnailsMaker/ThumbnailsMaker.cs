using Microsoft.Extensions.FileProviders;
using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FileOrganizer.Core.Helpers
{
    public sealed class ThumbnailsMaker : IThumbnailsMaker
    {
        public Image MakeThumb( IFileInfo fileInfo, Size size )
        {
            using var stream = fileInfo.CreateReadStream();
            using Image srcImage = Image.FromStream( stream );

            Size newSize  = ResizeKeepAspect( srcImage.Size, size.Width, size.Height, true );

            return ResizeImage( srcImage, newSize.Width, newSize.Height );
        }
        
        //====== from StackOverflow

        private static Bitmap ResizeImage( Image image, int newWidth, int newHeight )
        {
            var dstRect  = new Rectangle( 0, 0, newWidth, newHeight );
            var dstImage = new Bitmap( newWidth, newHeight );

            dstImage.SetResolution( image.HorizontalResolution, image.VerticalResolution );

            using (var grp = Graphics.FromImage( dstImage ))
            {
                grp.CompositingMode    = CompositingMode.SourceCopy;
                grp.CompositingQuality = CompositingQuality.HighQuality;
                grp.InterpolationMode  = InterpolationMode.HighQualityBicubic;
                grp.SmoothingMode      = SmoothingMode.HighQuality;
                grp.PixelOffsetMode    = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();

                wrapMode.SetWrapMode( WrapMode.TileFlipXY );

                grp.DrawImage( image, dstRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode );
            }

            return dstImage;
        }

        private static Size ResizeKeepAspect( Size srcSize, int maxWidth, int maxHeight, bool enlarge )
        {
            maxWidth  = enlarge ? maxWidth  : Math.Min( maxWidth,  srcSize.Width );
            maxHeight = enlarge ? maxHeight : Math.Min( maxHeight, srcSize.Height );

            decimal scale = Math.Min( maxWidth / (decimal) srcSize.Width, maxHeight / (decimal) srcSize.Height);

            int w = (int) Math.Round( srcSize.Width  * scale );
            int h = (int) Math.Round( srcSize.Height * scale );

            return new Size( w, h );
        }
    }
}
