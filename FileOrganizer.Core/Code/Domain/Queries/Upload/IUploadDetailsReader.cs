using FileOrganizer.Core.Services;

namespace FileOrganizer.Core
{
    public interface IUploadDetailsReader
    {
        UploadDetails GetUploadDetails( UploadId uploadId );
    }
}
