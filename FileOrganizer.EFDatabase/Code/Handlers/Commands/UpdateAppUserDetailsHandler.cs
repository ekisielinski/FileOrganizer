using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class UpdateAppUserDetailsHandler : IRequestHandler<UpdateAppUserDetailsCommand>
    {
        readonly EFAppContext context;
        readonly IActivityLogger logger;

        //====== ctors

        public UpdateAppUserDetailsHandler( EFAppContext context, IActivityLogger logger )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
            this.logger  = Guard.NotNull( logger,  nameof( logger  ) );
        }

        //====== IRequestHandler

        public async Task<Unit> Handle( UpdateAppUserDetailsCommand request, CancellationToken cancellationToken )
        {
            if (request.CanSkipExecution) return Unit.Value;

            AppUserEntity appUserEntity = await context.Entities
                .AppUsers
                .Where( x => x.UserName == request.UserName.Value )
                .FirstOrDefaultAsync();

            if (appUserEntity is null) throw new Exception( "App user does not exist: " + request.UserName ); // TODO: custom exception

            bool setDisplayName = request.DisplayName != null;

            if (!request.EmailAddress.Ignore) appUserEntity.EmailAddress = request.EmailAddress.Data?.Value;
            if (setDisplayName) appUserEntity.DisplayName = request.DisplayName!.Value;

            var messages = new List<string>();

            if (request.EmailAddress.Ignore == false) messages.Add( $"Email updated for user '{request.UserName}'. New value: " + (request.EmailAddress.Data?.ToString() ?? "<empty>") );
            if (setDisplayName) messages.Add( $"Display name updated for user '{request.UserName}'. New value: {request.DisplayName}" );

            logger.Add( string.Join( "\r\n", messages ) );

            await context.Entities.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
