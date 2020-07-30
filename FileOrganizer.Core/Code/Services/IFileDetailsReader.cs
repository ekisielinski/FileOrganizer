namespace FileOrganizer.Core.Services
{
    public interface IFileDetailsReader
    {
        FileDetails? GetFileDetailsById( FileId fileId );
    }
}
