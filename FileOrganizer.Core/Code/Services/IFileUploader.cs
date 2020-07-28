using System.IO;

namespace FileOrganizer.Core.Services
{
    public interface IFileUploader
    {
        FileId Upload( Stream content, MimeType mimeType, string? fileName );
    }
}
