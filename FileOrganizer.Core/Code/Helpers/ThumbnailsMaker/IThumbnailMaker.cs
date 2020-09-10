using Microsoft.Extensions.FileProviders;

namespace FileOrganizer.Core.Helpers
{
    public interface IThumbnailMaker
    {
        ThumbnailMakerResult? TryMakeAndSaveThumbnail( IFileInfo fileInfo, UtcTimestamp timestamp );
    }
}
