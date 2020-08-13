using Microsoft.Extensions.FileProviders;
using System.Drawing;

namespace FileOrganizer.Core.Helpers
{
    public interface IThumbnailsMaker
    {
        Image MakeThumb( IFileInfo fileInfo, Size size );
    }
}
