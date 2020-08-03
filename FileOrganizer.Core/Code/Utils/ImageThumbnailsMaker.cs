using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace FileOrganizer.Core.Utils
{
    public static class ImageThumbnailsMaker
    {
        public static Image? MakeThumb( Stream imageData, Size size )
        {
            try
            {
                using Image srcImage = Image.FromStream( imageData );

                Size newSize  = ResizeKeepAspect( srcImage.Size, size.Width, size.Height, true );

                Image thumbnail = ResizeImage (srcImage, newSize.Width, newSize.Height);

                return thumbnail;
            }
            catch
            {
                return null;
            }
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
