namespace FileOrganizer.Core
{
    public interface ICredentialsValidator
    {
        AppUser? ValidateUser( UserName name, UserPassword password );
    }
}
