using FileOrganizer.Core;
using Microsoft.Extensions.FileProviders;

namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileStorageReader
    {
        IFileInfo Get( FileName fileName );
    }
}
