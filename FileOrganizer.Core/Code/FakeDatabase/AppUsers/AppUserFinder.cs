using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class AppUserFinder : IAppUserFinder
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public AppUserFinder( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        //====== IAppUserFinder

        public IReadOnlyList<AppUser> GetAllAppUsers()
            => database.Users.Select( x => x.AppUserDetails.User ).ToList();
    }
}
