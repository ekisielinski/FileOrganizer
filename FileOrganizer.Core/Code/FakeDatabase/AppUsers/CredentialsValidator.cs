using FileOrganizer.Core.Helpers;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class CredentialsValidator : ICredentialsValidator
    {
        readonly FakeDatabaseSingleton database;
        readonly IPasswordHasher passwordHasher;

        //====== ctors

        public CredentialsValidator( FakeDatabaseSingleton database, IPasswordHasher passwordHasher )
        {
            this.database = database;
            this.passwordHasher = passwordHasher;
        }

        //====== ICredentialsValidator

        public AppUser? TryGetUser( UserName name, UserPassword password )
        {
            var user = database.Users.FirstOrDefault( x => x.AppUserDetails.User.Name.Value == name.Value );
            if (user is null) return null;

            bool ok = passwordHasher.VerifyHash( user.PasswordHash, password );

            return ok ? user.AppUserDetails.User : null;
        }
    }
}
