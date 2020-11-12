using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Domain
{
    public sealed class FileMetadataContainer
    {
        public FileMetadataContainer( IEnumerable<FileMetadataGroup> groups )
        {
            Groups = ArgUtils.ToRoList( groups, nameof( groups ) );
        }

        //====== public properties

        public IReadOnlyList<FileMetadataGroup> Groups { get; }

        public bool IsEmpty => Groups.Count == 0;

        //====== public static properties

        public static FileMetadataContainer Empty { get; } = new( Enumerable.Empty<FileMetadataGroup>() );
    }
}
