using Microsoft.Extensions.FileProviders;
using System.Drawing;
using FileOrganizer.CommonUtils;
using System.IO;
using System.Drawing.Imaging;

namespace FileOrganizer.Core.Helpers
{
    public sealed class ThumbnailMaker : IThumbnailMaker
    {
        readonly IImageResizer imageResizer;
        readonly IFileContainer fileContainer;

        //====== ctors

        public ThumbnailMaker( IImageResizer imageResizer, IFileContainer fileContainer )
        {
            this.imageResizer  = Guard.NotNull( imageResizer, nameof( imageResizer ) );
            this.fileContainer = Guard.NotNull( fileContainer, nameof( fileContainer ) );
        }

        //====== IThumbnailsMaker

        public ThumbnailMakerResult? TryMakeAndSaveThumbnail( IFileInfo fileInfo, UtcTimestamp timestamp )
        {
            Guard.NotNull( fileInfo, nameof( fileInfo ) );
            Guard.NotNull( timestamp, nameof( timestamp ) );

            try
            {
                using var stream = fileInfo.CreateReadStream();
                using Image srcImage = Image.FromStream( stream );

                using Image thumbnail = imageResizer.Resize( srcImage, new Size( 300, 300 ), true );

                string thumbFileName = SaveThumbnail( thumbnail, timestamp );

                return new ThumbnailMakerResult( new FileName( thumbFileName ), srcImage.Size );
            }
            catch
            {
                return null; // TODO: error logging
            }
        }

        private string SaveThumbnail( Image thumbnail, UtcTimestamp timestamp )
        {
            using var memoryStream = new MemoryStream( 50 * 1024 );

            thumbnail.Save( memoryStream, ImageFormat.Jpeg );

            string newFileName = FileUtils.GetRandomFileNameWithTimestamp( timestamp.Value, "_.jpg" );

            var thumbFile = fileContainer.Create( memoryStream, new FileName( newFileName ) );

            return thumbFile.Name;
        }
    }
}
