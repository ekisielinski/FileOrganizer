using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core.Services
{
    public sealed class DatabaseFiles
    {
        // TODO: move thumbnail to image details

        public DatabaseFiles( FileName source, FileName? thumbnail )
        {
            Source    = Guard.NotNull( source, nameof( source ) );
            Thumbnail = thumbnail;
        }

        //====== public properties

        public FileName  Source    { get; }
        public FileName? Thumbnail { get; }
    }
}
