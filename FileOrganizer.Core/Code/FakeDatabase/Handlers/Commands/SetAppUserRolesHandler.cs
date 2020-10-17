using FileOrganizer.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class SetAppUserRolesHandler : IRequestHandler<SetAppUserRolesCommand>
    {
        readonly FakeDatabaseSingleton database;
        readonly IActivityLogger logger;

        //====== ctors

        public SetAppUserRolesHandler( FakeDatabaseSingleton database, IActivityLogger logger )
        {
            this.database = database;
            this.logger = logger;
        }

        public async Task<Unit> Handle( SetAppUserRolesCommand request, CancellationToken cancellationToken )
        {
            UserEntry entry = database.Users.Single( x => x.AppUserDetails.User.Name.Value == request.UserName.Value );
            
            database.Users.Remove( entry );

            var newUser = new AppUser(
                entry.AppUserDetails.User.Name,
                entry.AppUserDetails.User.DisplayName,
                request.UserRoles
                );

            var newEntry = new UserEntry
            {
                AppUserDetails = new AppUserDetails( newUser, entry.AppUserDetails.Email, entry.AppUserDetails.WhenCreated ),
                PasswordHash = entry.PasswordHash
            };

            database.Users.Add( newEntry );

            logger.Add( $"Roles updated for user '{request.UserName}'. New value: " + request.UserRoles );

            return await Unit.Task;
        }
    }
}
