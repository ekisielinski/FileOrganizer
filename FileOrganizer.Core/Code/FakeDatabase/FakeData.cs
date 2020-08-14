using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core
{
    public static class FakeData
    {
        public static IReadOnlyList<AppUser> CreateAppUsers()
        {
            return new AppUser[]
            {
                new AppUser(
                    new UserName( "admin" ),
                    new UserDisplayName( "administrator" ),
                    new UserRoles( new[] { "administrator", "moderator" } )
                    ),

                new AppUser(
                    new UserName( "mod" ),
                    new UserDisplayName( "moderator" ),
                    new UserRoles( new[] { "moderator" } )
                    ),

                new AppUser(
                    new UserName( "user" ),
                    new UserDisplayName( "some user" ),
                    new UserRoles( Enumerable.Empty<string>() )
                    )
            };
        }
    }
}
