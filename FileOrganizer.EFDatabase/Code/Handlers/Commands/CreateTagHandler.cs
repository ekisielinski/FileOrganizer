using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class CreateTagHandler : IRequestHandler<CreateTagCommand>
    {
        readonly EFAppContext context;
        readonly IActivityLogger logger;

        //====== ctors

        public CreateTagHandler( EFAppContext context, IActivityLogger logger )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
            this.logger  = Guard.NotNull( logger, nameof( logger ) );
        }

        //====== IRequestHandler

        public async Task<Unit> Handle( CreateTagCommand request, CancellationToken cancellationToken )
        {
            var entity = new TagEntity
            {
                Name         = request.TagName.Value,
                DisplayName  = request.DisplayName.IsEmpty ? null : request.DisplayName.Value,
                Description  = request.Description.IsEmpty ? null : request.Description.Value,
                UtcWhenAdded = context.UtcNow.Value,
                CreatedBy    = await context.Entities.AppUsers.SingleAsync( x => x.UserName == context.Requestor.UserName.Value )
            };

            await context.Entities.Tags.AddAsync( entity );

            logger.Add( "Tag created: " + request.TagName );

            return Unit.Value;
        }
    }
}
