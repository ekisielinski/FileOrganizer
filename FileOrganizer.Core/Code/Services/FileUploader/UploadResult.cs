using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.Services
{
    public sealed class UploadResult
    {
        public UploadResult( UploadId id, IEnumerable<FileId> fileIds)
        {
            Id = Guard.NotNull( id, nameof( id ) );

            Guard.NotNull( fileIds, nameof( fileIds ) );
            FileIds = fileIds.ToList();
        }

        //====== public properties

        public UploadId Id { get; }

        public IReadOnlyList<FileId> FileIds { get; }
    }
}
