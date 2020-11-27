using FileOrganizer.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Domain
{
    public abstract class FileMetadataContainer
    {
        public abstract IReadOnlyList<FileMetadataGroup> Groups { get; }

        public bool IsEmpty => Groups.Count == 0;

        //====== public methods

        public string? TryGetValue( string groupName, string entryName )
        {
            Guard.NotNull( groupName, nameof( groupName ) );
            Guard.NotNull( entryName, nameof( entryName ) );

            return Groups.FirstOrDefault( x => x.Name == groupName )?.Entries.FirstOrDefault( x => x.Name == entryName )?.Value;
        }

        //====== public virtual methods

        public virtual UtcTimestamp? TryGetGpsTimestamp() => null;

        //====== public static properties

        public static FileMetadataContainer Empty { get; } = new EmptyFileMetadataContainer();

        //====== helper class

        private sealed class EmptyFileMetadataContainer : FileMetadataContainer
        {
            public override IReadOnlyList<FileMetadataGroup> Groups { get; } = Array.Empty<FileMetadataGroup>();
        }
    }
}
