using System.Drawing;
using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core.Helpers
{
    public sealed class ThumbnailMakerResult
    {
        public ThumbnailMakerResult( Image thumbnail, Size fullSize )
        {
            Thumbnail = Guard.NotNull( thumbnail, nameof( thumbnail ) ); ;
            
            FullSize = fullSize;
        }

        //====== public properties

        public Image Thumbnail { get; }

        public Size FullSize { get; }
    }
}
