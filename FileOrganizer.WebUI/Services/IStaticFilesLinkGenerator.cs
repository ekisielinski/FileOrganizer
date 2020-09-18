using FileOrganizer.Core;

namespace FileOrganizer.WebUI.Services
{
    public interface IStaticFilesLinkGenerator
    {
        string GetSourceFilePath( FileName fileName );

        string GetThumbnailPath( FileName fileName );
    }
}
