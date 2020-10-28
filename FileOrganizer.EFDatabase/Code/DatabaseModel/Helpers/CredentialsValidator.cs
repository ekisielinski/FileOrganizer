using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FileOrganizer.EFDatabase
{
    // todo: make command?
    public sealed class CredentialsValidator : ICredentialsValidator
    {
        readonly EFAppContext context;
        readonly IPasswordHasher passwordHasher;

        //====== ctors

        public CredentialsValidator( EFAppContext context, IPasswordHasher passwordHasher )
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        //====== ICredentialsValidator

        public AppUser? TryGetUser( UserName name, UserPassword password )
        {
            AppUserEntity? entity = context.Entities.AppUsers
                .Include( x => x.UserRoles )
                .AsNoTracking()
                .FirstOrDefault( x => x.UserName == name.Value );

            if (entity is null) return null;

            bool ok = passwordHasher.VerifyHash( entity.PasswordHash, password );

            return ok ? MappingUtils.ToAppUser( entity ) : null;
        }
    }
}
