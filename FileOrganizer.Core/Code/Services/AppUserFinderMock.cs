using System.Linq;

namespace FileOrganizer.Core
{
    public sealed class AppUserFinderMock : IAppUserFinder
    {
        public AppUser? Find( UserName userName, string password )
        {
            if (userName.Value == "admin") // any pass
            {
                return new AppUser(
                    userName,
                    new UserDisplayName( "administrator" ),
                    new UserRoles( new[] { "administrator", "moderator" } )
                    );
            }

            if (userName.Value == "mod") // any pass
            {
                return new AppUser(
                    userName,
                    new UserDisplayName( "moderator" ),
                    new UserRoles( new[] { "moderator" } )
                    );
            }

            if (userName.Value == "user") // any pass
            {
                return new AppUser(
                    userName,
                    new UserDisplayName( "some user" ),
                    new UserRoles( Enumerable.Empty<string>() )
                    );
            }

            return null;
        }
    }
}
