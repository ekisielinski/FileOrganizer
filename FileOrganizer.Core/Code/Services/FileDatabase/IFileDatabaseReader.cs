using FileOrganizer.Core.Services.Internal;

namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileDatabaseReader
    {
        IFileStorageReader GetStorageReader( FileDatabaseFolder folder );
    }
}
