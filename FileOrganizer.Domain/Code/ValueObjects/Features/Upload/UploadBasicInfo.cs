using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    // TODO: immutable
    public sealed class UploadBasicInfo
    {
        public UploadId          Id           { get; set; }

        public AppUserNames      Uploader     { get; set; }
        public UtcTimestamp      WhenUploaded { get; set; }

        public UploadDescription Description  { get; set; }

        public DataSize          TotalSize    { get; set; }
        public int               FileCount    { get; set; }
    }
}
