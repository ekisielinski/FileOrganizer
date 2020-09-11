namespace FileOrganizer.Core
{
    public interface IAppUserReader : IDomainQuery
    {
        AppUser GetUser( UserName userName );

        AppUserDetails GetUserDetails( UserName userName );
    }
}
