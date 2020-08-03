using FileOrganizer.Core.Utils;
using Microsoft.Extensions.FileProviders;
using System.Drawing;

namespace FileOrganizer.Core.Services
{
    public sealed class ThumbnailsMaker : IThumbnailsMaker
    {
        public Image? MakeThumb( IFileInfo fileInfo, Size size )
        {
            using var stream = fileInfo.CreateReadStream();

            return ImageThumbnailsMaker.MakeThumb( stream, size );
        }
    }
}
