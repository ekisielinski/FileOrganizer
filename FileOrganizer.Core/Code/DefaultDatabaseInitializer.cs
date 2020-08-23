using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class DefaultDatabaseInitializer : IDatabaseInitializer
    {
        readonly IAppUserCreator appUserCreator;

        //====== ctors

        public DefaultDatabaseInitializer( IAppUserCreator appUserCreator )
        {
            this.appUserCreator = Guard.NotNull( appUserCreator, nameof( appUserCreator ) );
        }

        //====== IDatabaseInitializer

        public void Init()
        {
            appUserCreator.Create( new AppUser( "admin", "Administrator", UserRoles.Administrator, UserRoles.Moderator ), new UserPassword( "admin" ) );
            appUserCreator.Create( new AppUser( "mod", "Moderator", UserRoles.Moderator ), new UserPassword( "mod" ) );
            appUserCreator.Create( new AppUser( "user", "User" ), new UserPassword( "user" ) );
        }
    }
}
