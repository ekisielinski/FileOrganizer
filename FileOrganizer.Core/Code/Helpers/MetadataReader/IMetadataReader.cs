using FileOrganizer.Domain;
using System.IO;

namespace FileOrganizer.Core.Helpers
{
    public interface IMetadataReader
    {
        FileMetadataContainer GetMetadata( Stream stream );
    }
}
