using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MetadataExtractor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core.Helpers
{
    public sealed class MetadataReader : IMetadataReader
    {
        public FileMetadataContainer GetMetadata( Stream stream )
        {
            Guard.NotNull( stream, nameof( stream ) );

            stream.Position = 0;

            IReadOnlyList<MetadataExtractor.Directory> metadata = ImageMetadataReader.ReadMetadata( stream );

            var groups = metadata.Select(ToGroup);
            return new FileMetadataContainer( groups );
        }

        //====== private methods

        private FileMetadataGroup ToGroup( MetadataExtractor.Directory dir )
        {
            return new( dir.Name, dir.Tags.Select( ToEntry ) );
        }

        private FileMetadataEntry ToEntry( MetadataExtractor.Tag tag )
        {
            return new( tag.Name, tag.Description ?? string.Empty );
        }
    }
}
