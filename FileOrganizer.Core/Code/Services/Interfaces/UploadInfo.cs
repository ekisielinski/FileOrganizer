using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class UploadInfo
    {
        public UploadId Id { get; set; }

        public UtcTimestamp WhenAdded { get; set; }
        public UploadDescription Description { get; set; }

        public UserName UserName { get; set; }
        public UserDisplayName DisplayName { get; set; }
        
        public DataSize TotalSize { get; set; }
        public int FileCount { get; set; }
    }
}
