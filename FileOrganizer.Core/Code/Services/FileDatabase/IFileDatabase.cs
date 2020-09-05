using FileOrganizer.Core.Helpers;

namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileDatabase : IFileDatabaseReader
    {
        IFileContainer GetContainer( FileDatabaseFolder folder );
    }
}
