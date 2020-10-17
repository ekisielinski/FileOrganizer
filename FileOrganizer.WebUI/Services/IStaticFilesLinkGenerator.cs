using FileOrganizer.Core;
using FileOrganizer.Domain;

namespace FileOrganizer.WebUI.Services
{
    public interface IStaticFilesLinkGenerator
    {
        string GetSourceFilePath( FileName fileName );

        string GetThumbnailPath( FileName fileName );
    }
}
