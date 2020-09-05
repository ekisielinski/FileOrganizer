using Microsoft.Extensions.FileProviders;

namespace FileOrganizer.Core.Helpers
{
    public interface IFileContainerReader
    {
        IFileInfo Get( FileName fileName );
    }
}
