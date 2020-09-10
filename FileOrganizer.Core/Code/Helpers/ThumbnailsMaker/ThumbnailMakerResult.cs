using System.Drawing;
using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core.Helpers
{
    public sealed class ThumbnailMakerResult
    {
        public ThumbnailMakerResult( FileName thumbFileName, Size orginalImageSize )
        {
            ThumbnailFileName = Guard.NotNull( thumbFileName, nameof( thumbFileName ) );

            OrginalImageSize = orginalImageSize;
        }

        //====== public properties

        public FileName ThumbnailFileName { get; }

        public Size OrginalImageSize { get; }
    }
}
