namespace FileOrganizer.Core
{
    public interface IAppUserCreator : IDomainCommand
    {
        void Create( AppUser appUser, UserPassword password );
    }
}
