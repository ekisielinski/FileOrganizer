using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class CreateAppUserHandler : IRequestHandler<CreateAppUserCommand>
    {
        readonly EFAppContext context;
        readonly IPasswordHasher passwordHasher;
        readonly IActivityLogger logger;

        //====== ctors

        public CreateAppUserHandler( EFAppContext context, IPasswordHasher passwordHasher, IActivityLogger logger )
        {
            this.context        = Guard.NotNull( context,        nameof( context        ) );
            this.passwordHasher = Guard.NotNull( passwordHasher, nameof( passwordHasher ) );
            this.logger         = Guard.NotNull( logger,         nameof( logger         ) );
        }

        //====== IRequestHandler

        public async Task<Unit> Handle( CreateAppUserCommand request, CancellationToken cancellationToken )
        {
            AppUserEntity appUserEntity = await context.Entities
                .AppUsers
                .Where( x => x.UserName == request.UserName.Value )
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (appUserEntity != null) throw new Exception( "User already exists: " + request.UserName ); // TODO: own exception

            string hash = passwordHasher.HashPassword( request.UserPassword );
            
            var appUser = new AppUser( request.UserName, request.DisplayName, UserRoles.Empty);
            var appUserDetails = new AppUserDetails( appUser, null, context.UtcNow );

            var newEntity = new AppUserEntity
            {
                UserName       = request.UserName.Value,    
                DisplayName    = request.DisplayName.Value,
                EmailAddress   = null,
                PasswordHash   = hash,
                UtcWhenCreated = context.UtcNow.Value
            };

            context.Entities.AppUsers.Add( newEntity );
            await context.Entities.SaveChangesAsync();

            logger.Add( "New app user created:" + request.UserName );

            return Unit.Value;
        }
    }
}
