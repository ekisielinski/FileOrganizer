using FileOrganizer.Domain;

namespace FileOrganizer.WebUI.Services.Auth
{
    public interface IAuthService : IAuthUserAccessor
    {
        bool Login( UserName userName, string password );

        void Logout();
    }
}
