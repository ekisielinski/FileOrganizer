using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileOrganizer.Core.Helpers
{
    public interface IFileContainer : IFileContainerReader
    {
        IFileInfo Create( Stream stream, FileName fileName );
    }
}
