using FileOrganizer.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetAppUsersHandler : IRequestHandler<GetAppUsersQuery, IReadOnlyList<AppUser>>
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public GetAppUsersHandler( FakeDatabaseSingleton database )
        {
            this.database = database;
        }
 
        public Task<IReadOnlyList<AppUser>> Handle( GetAppUsersQuery request, CancellationToken cancellationToken )
        {
            IReadOnlyList<AppUser> result = database.Users.Select( x => x.AppUserDetails.User ).ToList();

            return Task.FromResult( result );
        }
    }
}
