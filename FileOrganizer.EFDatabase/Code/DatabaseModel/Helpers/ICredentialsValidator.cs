using FileOrganizer.Domain;

namespace FileOrganizer.EFDatabase
{
    public interface ICredentialsValidator
    {
        AppUser? TryGetUser( UserName name, UserPassword password );
    }
}
