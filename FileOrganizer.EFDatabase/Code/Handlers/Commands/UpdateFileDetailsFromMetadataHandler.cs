using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class UpdateFileDetailsFromMetadataHandler : IRequestHandler<UpdateFileDetailsFromMetadataCommand>
    {
        readonly ISender sender;

        //====== ctors

        public UpdateFileDetailsFromMetadataHandler( ISender sender )
        {
            this.sender = Guard.NotNull( sender, nameof( sender ) );
        }

        //====== IRequestHandler

        public async Task<Unit> Handle( UpdateFileDetailsFromMetadataCommand request, CancellationToken cancellationToken )
        {
            if (request.CanSkipExecution) return Unit.Value;

            // TODO: should not run commands within commands (?)

            FileMetadataContainer result = await sender.Send( new GetMetadataFromFileContentQuery( request.FileId ), cancellationToken );

            if (request.UpdatePrimaryDateTime)
            {
                UtcTimestamp? creationTimestamp = result.TryGetGpsTimestamp();

                if (creationTimestamp is not null)
                {
                    var partialDateTime = PartialDateTime.FromUtcDateTime( creationTimestamp.Value );

                    await sender.Send( new UpdateFileDetailsCommand( request.FileId ) { PrimaryDateTime = partialDateTime }, cancellationToken );
                }
            }

            return Unit.Value;
        }
    }
}
