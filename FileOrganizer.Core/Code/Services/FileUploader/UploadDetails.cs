using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.Services
{
    public sealed class UploadDetails
    {
        public UploadDetails( UploadId id, IEnumerable<FileDetails> files, UploadDescription description )
        {
            Guard.NotNull( id, nameof( id ) );
            Guard.NotNull( files, nameof( files ) );
            Guard.NotNull( description, nameof( description ) );

            Id = id;
            Files = ArgUtils.ToRoList( files, nameof( files ) );
            Description = description;
        }

        //====== public properties

        public UploadId Id { get; }

        public IReadOnlyList<FileDetails> Files { get; }

        public UploadDescription Description { get; }

        public DataSize UploadSize => Files.Select( x => x.FileSize ).Aggregate( DataSize.Sum );
    }
}
