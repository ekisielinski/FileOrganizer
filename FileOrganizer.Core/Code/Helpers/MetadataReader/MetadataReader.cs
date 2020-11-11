using FileOrganizer.CommonUtils;
using MetadataExtractor;
using System.Collections.Generic;
using System.IO;

namespace FileOrganizer.Core.Helpers
{
    public sealed class MetadataReader : IMetadataReader
    {
        public IReadOnlyList<MetadataExtractor.Directory> GetMetadata( Stream stream )
        {
            Guard.NotNull( stream, nameof( stream ) );

            stream.Position = 0;
            
            return ImageMetadataReader.ReadMetadata( stream );
        }
    }
}
