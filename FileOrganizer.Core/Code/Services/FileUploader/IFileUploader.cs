namespace FileOrganizer.Core.Services
{
    public interface IFileUploader
    {
        UploadId Upload( UploadInfo[] uploads, UploadDescription description );
    }
}
