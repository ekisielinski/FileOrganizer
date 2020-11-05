using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class AddFileLinkHanlder : IRequestHandler<AddFileLinkCommand, LinkId>
    {
        readonly EFAppContext context;
        readonly IActivityLogger logger;

        //====== ctors

        public AddFileLinkHanlder( EFAppContext context, IActivityLogger logger )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
            this.logger  = Guard.NotNull( logger, nameof( logger ) );
        }

        //====== IRequestHandler

        public async Task<LinkId> Handle( AddFileLinkCommand request, CancellationToken cancellationToken )
        {
            var newEntity = new FileLinkEntity
            {
                //FileId = request.FileId.Value,
                File = context.Entities.Files.FirstOrDefault(x => x.Id == request.FileId.Value),

                Address = request.Address.Value,
                Title = request.Title?.Value,
                Comment = request.Comment?.Value,

                UtcWhenAdded = context.UtcNow.Value,

                Issuer = context.Entities.AppUsers.FirstOrDefault( x => x.UserName == context.Requestor.UserName.Value )
            };

            await context.Entities.FileLinks.AddAsync( newEntity );

            logger.Add( "Created new file link: " + newEntity.Address + " #" + newEntity.Id );

            return new LinkId( newEntity.Id );
        }
    }
}
