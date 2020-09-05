namespace FileOrganizer.Core
{
    public interface IAppUserUpdater : IDomainCommand
    {
        void SetEmail( UserName userName, EmailAddress? email );

        void SetDisplayName( UserName userName, UserDisplayName displayName );
    }
}
