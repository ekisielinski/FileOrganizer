using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileStorage : IFileStorageReader
    {
        IFileInfo Create( Stream stream, FileName fileName );
    }
}
