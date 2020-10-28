using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
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
