using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Domain
{
    public sealed class UploadDetails
    {
        public UploadDetails( UploadId id, IEnumerable<FileDetails> files, UploadDescription description, IReadOnlyList<string> rejectedDuplicates )
        {
            Id                 = Guard.NotNull( id, nameof( id ) );
            Files              = ArgUtils.ToRoList( files, nameof( files ) );
            Description        = Guard.NotNull( description, nameof( description ) );
            RejectedDuplicates = Guard.NotNull( rejectedDuplicates, nameof( rejectedDuplicates ) );
        }

        //====== public properties

        public UploadId Id { get; }

        public IReadOnlyList<FileDetails> Files { get; }

        public UploadDescription Description { get; }

        public IReadOnlyList<string> RejectedDuplicates { get; }

        public DataSize UploadSize => Files.Select( x => x.FileSize ).Aggregate( DataSize.Zero, DataSize.Sum );
    }
}
