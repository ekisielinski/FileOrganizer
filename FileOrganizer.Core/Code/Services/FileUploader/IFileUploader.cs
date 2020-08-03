namespace FileOrganizer.Core.Services
{
    public interface IFileUploader
    {
        UploadResult Upload( UploadInfo[] uploads );
    }
}
