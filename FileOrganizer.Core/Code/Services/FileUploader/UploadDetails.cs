using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.Services
{
    public sealed class UploadDetails
    {
        public UploadDetails( UploadId id, IEnumerable<FileDetails> files, string description )
        {
            Guard.NotNull( id, nameof( id ) );
            Guard.NotNull( files, nameof( files ) );
            Guard.NotNull( description, nameof( description ) );

            Id = id;
            Files = files.ToList();
            Description = description;
        }

        //====== public properties

        public UploadId Id { get; }

        public IReadOnlyList<FileDetails> Files { get; }

        public string Description { get; }
    }
}
