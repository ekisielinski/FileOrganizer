namespace FileOrganizer.Core
{
    public interface ICredentialsValidator : IDomainQuery
    {
        AppUser? TryGetUser( UserName name, UserPassword password );
    }
}
