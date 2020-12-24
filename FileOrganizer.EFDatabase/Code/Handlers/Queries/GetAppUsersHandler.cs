using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class GetAppUsersHandler : IRequestHandler<GetAppUsersQuery, IReadOnlyList<AppUser>>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetAppUsersHandler( EFAppContext context )
            => this.context = Guard.NotNull( context, nameof( context ) );

        //====== IRequestHandler

        public async Task<IReadOnlyList<AppUser>> Handle( GetAppUsersQuery request, CancellationToken cancellationToken )
        {
            List<AppUserEntity> entities = await context.Entities
                .AppUsers
                .AsNoTracking()
                .Include( x => x.UserRoles )
                .ToListAsync();

            return entities.Select( MappingUtils.ToAppUser ).ToList();
        }
    }
}
