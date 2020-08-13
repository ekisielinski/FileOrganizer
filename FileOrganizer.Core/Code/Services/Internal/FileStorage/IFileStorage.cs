using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileOrganizer.Core.Services.Internal
{
    public interface IFileStorage : IFileStorageReader
    {
        IFileInfo Create( Stream stream, FileName fileName );
    }
}
