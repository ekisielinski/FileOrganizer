using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using FileOrganizer.EFDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetAppUserDetailsHandler : IRequestHandler<GetAppUserDetailsQuery, AppUserDetails>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetAppUserDetailsHandler( EFAppContext context )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
        }

        //====== IRequestHandler

        public async Task<AppUserDetails> Handle( GetAppUserDetailsQuery request, CancellationToken cancellationToken )
        {
            AppUserEntity? entity = await context.Entities
                .AppUsers
                .Include( x => x.UserRoles )
                .AsNoTracking()
                .FirstOrDefaultAsync( x => x.UserName == request.UserName.Value );

            if (entity is null) throw new System.Exception( "User not found." ); // todo: custom ex

            return MappingUtils.ToAppUserDetails( entity );
        }
    }
}
