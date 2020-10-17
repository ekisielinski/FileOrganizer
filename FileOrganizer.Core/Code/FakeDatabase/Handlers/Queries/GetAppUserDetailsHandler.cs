using FileOrganizer.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetAppUserDetailsHandler : IRequestHandler<GetAppUserDetailsQuery, AppUserDetails>
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public GetAppUserDetailsHandler( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        //====== IAppUserReader

        public Task<AppUserDetails> Handle( GetAppUserDetailsQuery request, CancellationToken cancellationToken )
        {
            var result = database.Users.Single( x => x.AppUserDetails.User.Name.Value == request.UserName.Value ).AppUserDetails;

            return Task.FromResult( result );
        }
    }
}
