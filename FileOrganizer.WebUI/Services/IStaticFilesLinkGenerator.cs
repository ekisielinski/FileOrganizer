using FileOrganizer.Services.FileDatabase;

namespace FileOrganizer.WebUI.Services
{
    public interface IStaticFilesLinkGenerator
    {
        string GetDatabaseFilePath( FileName fileName, FileDatabaseFolder folder );
    }
}
