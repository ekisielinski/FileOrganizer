using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using System;

namespace FileOrganizer.Core.FakeDatabase
{
    internal sealed class FileEntry
    {
        public int             Id            { get; set; } = -1;
        public UploadId        UploadId      { get; set; }
        public MimeType        MimeType      { get; set; } = MimeType.Unknown;
        public string?         FileName      { get; set; } = null;
        public UtcTimestamp    WhenAdded     { get; set; } = new UtcTimestamp( DateTime.UtcNow );
        public DatabaseFiles   DatabaseFiles { get; set; }
        public DataSize        Size          { get; set; }
        public FileDescription Description   { get; set; } = FileDescription.Empty;
        public FileTitle       Title         { get; set; } = FileTitle.Empty;
        public ImageDetails?   ImageDetails  { get; set; } = null;
        public Sha256Hash      Hash          { get; set; }
        public UserName        Uploader      { get; set; }
    }
}
