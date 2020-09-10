using Microsoft.Extensions.FileProviders;
using System.Drawing;

namespace FileOrganizer.Core.Helpers
{
    public interface IThumbnailMaker
    {
        ThumbnailMakerResult MakeThumb( IFileInfo fileInfo, Size size );
    }
}
