using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class AppUserReader : IAppUserReader
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public AppUserReader( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        //====== IAppUserReader

        public AppUser GetUser( UserName userName )
            => database.Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value ).AppUserDetails.User;

        public AppUserDetails GetUserDetails( UserName userName )
            => database.Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value ).AppUserDetails;
    }
}
