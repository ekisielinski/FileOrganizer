using Microsoft.Extensions.FileProviders;

namespace FileOrganizer.Core.Services.Internal
{
    public interface IFileStorageReader
    {
        IFileInfo Get( FileName fileName );
    }
}
