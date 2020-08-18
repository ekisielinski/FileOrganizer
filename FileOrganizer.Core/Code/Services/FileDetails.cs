using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core.Services
{
    public sealed class FileDetails
    {
        public FileId FileId { get; set; }

        public DatabaseFiles DatabaseFiles { get; set; }

        public DataSize FileSize { get; set; }

        public FileTitle Title { get; set; } = FileTitle.Empty;
        public FileDescription Description { get; set; } = FileDescription.Empty;

        // TODO: immutable
    }
}
