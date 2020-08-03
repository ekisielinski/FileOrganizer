using System.Collections.Generic;

namespace FileOrganizer.Core.Services
{
    public interface IFileDetailsReader
    {
        FileDetails? GetFileDetailsById( FileId fileId );

        IReadOnlyList<FileDetails> GetFileDetailsByUploadId( UploadId uploadId );
    }
}
