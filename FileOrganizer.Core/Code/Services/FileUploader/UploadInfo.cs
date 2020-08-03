using FileOrganizer.CommonUtils;
using System.IO;

namespace FileOrganizer.Core.Services
{
    public sealed class UploadInfo
    {
        public UploadInfo( Stream content, MimeType mimeType, string? fileName )
        {
            Content  = Guard.NotNull( content, nameof( content ) );
            MimeType = Guard.NotNull( mimeType, nameof( mimeType ) );
            FileName = fileName;
        }

        //====== public properties

        public Stream   Content  { get; }
        public MimeType MimeType { get; }
        public string?  FileName { get; }
    }
}
