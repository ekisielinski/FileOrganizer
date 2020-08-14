namespace FileOrganizer.Core
{
    public interface IAppUserFinder
    {
        AppUser? Find( UserName userName, string password );

        AppUser? Find( UserName userName );
    }
}
