using FileOrganizer.Core.Helpers;

namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileDatabase : IFileDatabaseReader
    {
        IFileContainer SourceFiles { get; }

        IFileContainer Thumbnails { get; }
    }
}
