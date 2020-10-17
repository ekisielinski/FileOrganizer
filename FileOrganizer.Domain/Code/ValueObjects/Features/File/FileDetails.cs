using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    // TODO: immutable
    public sealed class FileDetails
    {
        public FileId          FileId          { get; set; }

        public AppUserNames    Uploader        { get; set; }
        public DatabaseFiles   DatabaseFiles   { get; set; }

        public FileTitle       Title           { get; set; } = FileTitle.Empty;
        public FileDescription Description     { get; set; } = FileDescription.Empty;
        public PartialDateTime PrimaryDateTime { get; set; } = PartialDateTime.Empty;

        public DataSize        FileSize        { get; set; }

        public ImageDetails    ImageDetails    { get; set; } = new ImageDetails();
    }
}
