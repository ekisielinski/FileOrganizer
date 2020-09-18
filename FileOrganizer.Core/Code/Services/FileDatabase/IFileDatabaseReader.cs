using FileOrganizer.Core.Helpers;

namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileDatabaseReader
    {
        IFileContainerReader SourceFilesReader { get; }

        IFileContainerReader ThumbnailsReader { get; }
    }
}
