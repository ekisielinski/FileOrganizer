using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.Helpers
{
    internal sealed class MetadataExtractorDirectories : FileMetadataContainer
    {
        readonly IReadOnlyList<MetadataExtractor.Directory> dirs;

        //====== ctors

        public MetadataExtractorDirectories( IEnumerable<MetadataExtractor.Directory> dirs )
        {
            this.dirs = ArgUtils.ToRoList( dirs, nameof( dirs ) );

            Groups = this.dirs.Select( ToGroup ).ToList();
        }

        //====== override: FileMetadataContainer

        public override IReadOnlyList<FileMetadataGroup> Groups { get; }

        public override UtcTimestamp? TryGetGpsTimestamp()
        {
            GpsDirectory? dir = dirs.OfType<GpsDirectory>().FirstOrDefault();

            if (dir is null) return null;

            return dir.TryGetGpsDate( out DateTime timestamp ) ? new( timestamp ) : null;
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
