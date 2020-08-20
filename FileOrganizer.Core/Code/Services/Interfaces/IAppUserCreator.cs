namespace FileOrganizer.Core
{
    public interface IAppUserCreator
    {
        void Create( AppUser appUser, UserPassword password );
    }
}
