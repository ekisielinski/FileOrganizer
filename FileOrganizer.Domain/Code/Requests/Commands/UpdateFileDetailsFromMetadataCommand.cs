using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class UpdateFileDetailsFromMetadataCommand : IRequest
    {
        public UpdateFileDetailsFromMetadataCommand( FileId fileId )
        {
            FileId = Guard.NotNull( fileId, nameof( fileId ) );
        }

        //====== public properties

        public FileId FileId { get; }

        public bool UpdatePrimaryDateTime { get; init; } = true;

        public bool CanSkipExecution => UpdatePrimaryDateTime == false;
    }
}
