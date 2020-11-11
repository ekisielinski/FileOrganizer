using System.Collections.Generic;
using System.IO;

namespace FileOrganizer.Core.Helpers
{
    public interface IMetadataReader
    {
        IReadOnlyList<MetadataExtractor.Directory> GetMetadata( Stream stream );
    }
}
