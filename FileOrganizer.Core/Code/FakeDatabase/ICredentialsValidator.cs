using FileOrganizer.Domain;

namespace FileOrganizer.Core
{
    public interface ICredentialsValidator
    {
        AppUser? TryGetUser( UserName name, UserPassword password );
    }
}
