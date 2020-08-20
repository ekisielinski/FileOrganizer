using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class UploadEntry
    {
        public UploadId          Id          { get; set; }
        public UploadDescription Description { get; set; } = UploadDescription.None;
        public UtcTimestamp      WhenAdded   { get; set; }
        public int               FileCount   { get; set; }
        public DataSize          Size        { get; set; }
        public UserName          UserName    { get; set; }
    }
}
