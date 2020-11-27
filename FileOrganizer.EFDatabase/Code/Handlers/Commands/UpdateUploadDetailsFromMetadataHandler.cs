using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class UpdateUploadDetailsFromMetadataHandler : IRequestHandler<UpdateUploadDetailsFromMetadataCommand>
    {
        readonly EFAppContext context;
        readonly ISender sender;
        readonly IActivityLogger logger;

        //====== ctors

        public UpdateUploadDetailsFromMetadataHandler( EFAppContext context, ISender sender, IActivityLogger logger )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
            this.sender  = Guard.NotNull( sender, nameof( sender ) );
            this.logger  = Guard.NotNull( logger, nameof( logger ) );
        }

        public async Task<Unit> Handle( UpdateUploadDetailsFromMetadataCommand request, CancellationToken cancellationToken )
        {
            UploadDetails uploadDetails = await sender.Send( new GetUploadDetailsQuery( request.UploadId ) );

            foreach (FileDetails fileDetails in uploadDetails.Files)
            {
                await sender.Send( new UpdateFileDetailsFromMetadataCommand( fileDetails.FileId ) );
            }

            return Unit.Value;
        }
    }
}
