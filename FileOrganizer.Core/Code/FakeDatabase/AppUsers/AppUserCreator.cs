using FileOrganizer.Core.Helpers;
using System;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class AppUserCreator : IAppUserCreator
    {
        readonly FakeDatabaseSingleton database;
        readonly IPasswordHasher passwordHasher;

        //====== ctors

        public AppUserCreator( FakeDatabaseSingleton database, IPasswordHasher passwordHasher )
        {
            this.database = database;
            this.passwordHasher = passwordHasher;
        }

        //====== IAppUserCreator

        public void Create( AppUser appUser, UserPassword password )
        {
            if (database.Users.Any( x => x.AppUserDetails.User.Name.Value == appUser.Name.Value ))
            {
                throw new Exception( "User already exists." );
            }

            string hash = passwordHasher.HashPassword( password );
            var appUserDetails = new AppUserDetails( appUser, null, database.UtcNow );

            database.Users.Add( new UserEntry { AppUserDetails = appUserDetails, PasswordHash = hash } );
        }
    }
}
