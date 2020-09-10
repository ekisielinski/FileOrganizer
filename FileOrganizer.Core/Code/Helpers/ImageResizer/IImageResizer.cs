using System.Drawing;

namespace FileOrganizer.Core.Helpers
{
    public interface IImageResizer
    {
        Image Resize( Image image, Size toSize, bool keepAspect );
    }
}
