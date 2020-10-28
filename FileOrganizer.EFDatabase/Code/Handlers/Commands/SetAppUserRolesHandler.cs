using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class SetAppUserRolesHandler : IRequestHandler<SetAppUserRolesCommand>
    {
        readonly EFAppContext context;
        readonly IActivityLogger logger;

        //====== ctors

        public SetAppUserRolesHandler( EFAppContext context, IActivityLogger logger )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
            this.logger  = Guard.NotNull( logger,  nameof( logger  ) );
        }

        //====== IRequestHandler

        public async Task<Unit> Handle( SetAppUserRolesCommand request, CancellationToken cancellationToken )
        {
            AppUserEntity appUserEntity = context.Entities
                .AppUsers
                .Where( x => x.UserName == request.UserName.Value )
                .Include( x => x.UserRoles )
                .FirstOrDefault();

            if (appUserEntity is null) throw new Exception( "App user does not exist: " + request.UserName ); // TODO: custom exception

            appUserEntity.UserRoles.Clear();

            foreach( var role in request.UserRoles.Items )
            {
                appUserEntity.UserRoles.Add( new UserRolesEntity
                {
                    RoleName = role.Value
                } );
            }

            await context.Entities.SaveChangesAsync();

            logger.Add( $"Roles updated for user '{request.UserName}'. New values: " + request.UserRoles );

            return Unit.Value;
        }
    }
}
