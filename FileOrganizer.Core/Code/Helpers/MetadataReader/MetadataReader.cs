using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MetadataExtractor;
using System.Collections.Generic;
using System.IO;

namespace FileOrganizer.Core.Helpers
{
    public sealed class MetadataReader : IMetadataReader
    {
        public FileMetadataContainer GetMetadata( Stream stream )
        {
            Guard.NotNull( stream, nameof( stream ) );

            stream.Position = 0;

            IReadOnlyList<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata( stream );

            return new MetadataExtractorDirectories( directories );
        }
    }
}
