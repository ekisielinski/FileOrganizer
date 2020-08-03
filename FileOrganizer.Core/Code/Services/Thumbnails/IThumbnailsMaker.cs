using Microsoft.Extensions.FileProviders;
using System.Drawing;

namespace FileOrganizer.Core.Services
{
    public interface IThumbnailsMaker
    {
        Image? MakeThumb( IFileInfo fileInfo, Size size );
    }
}
