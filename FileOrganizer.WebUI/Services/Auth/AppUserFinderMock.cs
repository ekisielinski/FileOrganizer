using System.Linq;

namespace FileOrganizer.WebUI.Services.Auth
{
    public sealed class AppUserFinderMock : IAppUserFinder
    {
        public AuthUser? Find( UserName userName, string password )
        {
            if (userName.Value == "admin") // any pass
            {
                return new AuthUser(
                    userName,
                    new UserDisplayName( "administrator" ),
                    new UserRoles( new[] { "administrator", "moderator" } )
                    );
            }

            if (userName.Value == "mod") // any pass
            {
                return new AuthUser(
                    userName,
                    new UserDisplayName( "moderator" ),
                    new UserRoles( new[] { "moderator" } )
                    );
            }

            if (userName.Value == "user") // any pass
            {
                return new AuthUser(
                    userName,
                    new UserDisplayName( "some user" ),
                    new UserRoles( Enumerable.Empty<string>() )
                    );
            }

            return null;
        }
    }
}
