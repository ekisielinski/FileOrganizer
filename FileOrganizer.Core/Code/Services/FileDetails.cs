namespace FileOrganizer.Core.Services
{
    public sealed class FileDetails
    {
        public FileId FileId { get; set; }
        public string FileNameInDatabase { get; set; }

        // TODO: immutable
    }
}
