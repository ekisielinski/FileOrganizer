using FileOrganizer.Core.Helpers;

namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileDatabaseReader
    {
        IFileContainerReader GetContainerReader( FileDatabaseFolder folder );
    }
}
