using FileOrganizer.Core;

namespace FileOrganizer.WebUI.Services.Auth
{
    public interface IAppUserFinder
    {
        // TODO: AppUser
        AuthUser? Find( UserName userName, string password );
    }
}
