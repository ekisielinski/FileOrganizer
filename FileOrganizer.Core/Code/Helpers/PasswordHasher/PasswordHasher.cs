using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using Microsoft.AspNetCore.Identity;

namespace FileOrganizer.Core.Helpers
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        public string HashPassword( UserPassword password )
        {
            Guard.NotNull( password, nameof( password ) );

            var hasher = new PasswordHasher<object>();

            return hasher.HashPassword( new object(), password.Value );
        }

        public bool VerifyHash( string hash, UserPassword password )
        {
            Guard.NotNull( hash, nameof( hash ) );
            Guard.NotNull( password, nameof( password ) );

            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword( new object(), hash, password.Value );

            return result != PasswordVerificationResult.Failed;
        }
    }
}
