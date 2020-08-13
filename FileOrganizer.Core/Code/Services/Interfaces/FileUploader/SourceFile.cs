using FileOrganizer.CommonUtils;
using System.IO;

namespace FileOrganizer.Core.Services
{
    public sealed class SourceFile
    {
        public SourceFile( Stream content, MimeType mimeType, string? orginalFileName )
        {
            Content  = Guard.NotNull( content, nameof( content ) );
            MimeType = Guard.NotNull( mimeType, nameof( mimeType ) );

            OrginalFileName = orginalFileName;
        }

        //====== public properties

        public Stream   Content         { get; }
        public MimeType MimeType        { get; }
        public string?  OrginalFileName { get; }
    }
}
