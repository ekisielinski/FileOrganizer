namespace FileOrganizer.Core.Services
{
    public sealed class FileDetails
    {
        public FileId FileId { get; set; }

        public DatabaseFiles DatabaseFiles { get; set; }

        // TODO: immutable
    }
}
