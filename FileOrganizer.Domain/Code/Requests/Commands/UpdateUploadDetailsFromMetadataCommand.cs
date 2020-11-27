using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class UpdateUploadDetailsFromMetadataCommand : IRequest
    {
        public UpdateUploadDetailsFromMetadataCommand( UploadId uploadId )
        {
            UploadId = Guard.NotNull( uploadId, nameof( uploadId ) );
        }

        //====== public properties

        public UploadId UploadId { get; }

        public bool UpdatePrimaryDateTime { get; init; } = true;

        public bool CanSkipExecution => UpdatePrimaryDateTime == false;
    }
}
