namespace FileOrganizer.WebUI.Services.Auth
{
    public interface IAuthService : IAuthUserAccessor
    {
        bool Login( string userName, string password );

        void Logout();
    }
}
