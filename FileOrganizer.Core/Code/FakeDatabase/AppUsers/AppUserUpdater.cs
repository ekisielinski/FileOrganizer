using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class AppUserUpdater : IAppUserUpdater
    {
        readonly FakeDatabaseSingleton database;
        readonly IActivityLogger logger;

        //====== ctors

        public AppUserUpdater( FakeDatabaseSingleton database, IActivityLogger logger )
        {
            this.database = database;
            this.logger = logger;
        }

        //====== IAppUserUpdater

        public void SetEmail( UserName userName, EmailAddress? email )
        {
            UserEntry entry = database.Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            database.Users.Remove( entry );

            var newEntry = new UserEntry
            {
                AppUserDetails = new AppUserDetails( entry.AppUserDetails.User, email, entry.AppUserDetails.WhenCreated )
            };

            database.Users.Add( newEntry );

            logger.Add( $"Email updated for user '{userName}'. New value: " + (email?.ToString() ?? "<empty>") );
        }

        public void SetDisplayName( UserName userName, UserDisplayName displayName )
        {
            UserEntry entry = database.Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            database.Users.Remove( entry );

            var newUser = new AppUser( entry.AppUserDetails.User.Name, displayName, entry.AppUserDetails.User.Roles );
            var newDetails = new AppUserDetails( newUser, entry.AppUserDetails.Email, entry.AppUserDetails.WhenCreated );

            database.Users.Add( new UserEntry { AppUserDetails = newDetails } );

            logger.Add( $"Display name updated for user '{userName}'. New value: {displayName}" );
        }
    }
}
