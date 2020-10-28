using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class UploadFilesCommand : IRequest<UploadId>
    {
        public UploadFilesCommand( UploadParameters parameters )
        {
            Parameters = Guard.NotNull( parameters, nameof( parameters ) );
        }

        //====== public properties

        public UploadParameters Parameters { get; }
    }
}
