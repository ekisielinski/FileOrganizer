using FileOrganizer.Core.Services;

namespace FileOrganizer.Core
{
    public interface IFileUploader : IDomainCommand
    {
        UploadId Upload( UploadParameters parameters );
    }
}
