namespace FileOrganizer.Core.Services
{
    public interface IFileUploader
    {
        UploadId Upload( UploadParameters parameters );
    }
}
