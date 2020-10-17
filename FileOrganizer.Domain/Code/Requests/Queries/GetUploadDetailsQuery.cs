using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;

namespace FileOrganizer.Core
{
    public sealed class GetUploadDetailsQuery : IRequest<UploadDetails>
    {
        public GetUploadDetailsQuery( UploadId uploadId )
        {
            UploadId = Guard.NotNull( uploadId, nameof( uploadId ) );
        }

        //====== public properties

        public UploadId UploadId { get; }
    }
}
