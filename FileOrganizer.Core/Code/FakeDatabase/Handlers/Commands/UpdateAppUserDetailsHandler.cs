using FileOrganizer.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class UpdateAppUserDetailsHandler : IRequestHandler<UpdateAppUserDetailsCommand>
    {
        readonly FakeDatabaseSingleton database;
        readonly IActivityLogger logger;

        //====== ctors

        public UpdateAppUserDetailsHandler( FakeDatabaseSingleton database, IActivityLogger logger )
        {
            this.database = database;
            this.logger = logger;
        }

        public Task<Unit> Handle( UpdateAppUserDetailsCommand request, CancellationToken cancellationToken )
        {
            if (request.CanSkipExecution) return Unit.Task;

            if (request.DeleteEmail || request.EmailAddress != null) SetEmail( request.UserName, request.EmailAddress );
            if (request.DisplayName != null) SetDisplayName( request.UserName, request.DisplayName );

            return Unit.Task;
        }

        private void SetEmail( UserName userName, EmailAddress? email )
        {
            UserEntry entry = database.Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            database.Users.Remove( entry );

            var newEntry = new UserEntry
            {
                AppUserDetails = new AppUserDetails( entry.AppUserDetails.User, email, entry.AppUserDetails.WhenCreated ),
                PasswordHash = entry.PasswordHash
            };

            database.Users.Add( newEntry );

            logger.Add( $"Email updated for user '{userName}'. New value: " + (email?.ToString() ?? "<empty>") );
        }

        private void SetDisplayName( UserName userName, UserDisplayName displayName )
        {
            UserEntry entry = database.Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            database.Users.Remove( entry );

            var newUser = new AppUser( entry.AppUserDetails.User.Name, displayName, entry.AppUserDetails.User.Roles );
            var newDetails = new AppUserDetails( newUser, entry.AppUserDetails.Email, entry.AppUserDetails.WhenCreated );

            database.Users.Add( new UserEntry { AppUserDetails = newDetails, PasswordHash = entry.PasswordHash } );

            logger.Add( $"Display name updated for user '{userName}'. New value: {displayName}" );
        }
    }
}
