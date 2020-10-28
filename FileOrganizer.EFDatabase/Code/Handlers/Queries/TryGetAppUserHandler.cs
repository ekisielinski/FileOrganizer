using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class TryGetAppUserHandler : IRequestHandler<TryGetAppUserQuery, AppUser?>
    {
        readonly EFAppContext context;
        readonly IPasswordHasher hasher;

        //====== ctors

        public TryGetAppUserHandler( EFAppContext context, IPasswordHasher hasher )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
            this.hasher  = Guard.NotNull( hasher,  nameof( hasher  ) );
        }

        //====== IRequestHandler

        public async Task<AppUser?> Handle( TryGetAppUserQuery request, CancellationToken cancellationToken )
        {
            AppUserEntity entity = await context.Entities
                .AppUsers
                .Include( x => x.UserRoles )
                .FirstOrDefaultAsync( x => x.UserName == request.UserName.Value );

            if (entity is null) return null;

            bool validPass = hasher.VerifyHash( entity.PasswordHash, request.Password );

            return validPass ? MappingUtils.ToAppUser( entity ) : null;
        }
    }
}
