using FileOrganizer.CommonUtils;
using System.Collections.Generic;

namespace FileOrganizer.Domain
{
    public sealed class FileMetadataGroup
    {
        public FileMetadataGroup( string name, IEnumerable<FileMetadataEntry> entries )
        {
            Name = Guard.NotNull( name, nameof( name ) );

            Entries = ArgUtils.ToRoList( entries, nameof( entries ) );
        }

        //====== public properties

        public string Name { get; }

        public IReadOnlyList<FileMetadataEntry> Entries { get; }

        //====== override: Object

        public override string ToString() => $"{Name} ({Entries.Count})";
    }
}
