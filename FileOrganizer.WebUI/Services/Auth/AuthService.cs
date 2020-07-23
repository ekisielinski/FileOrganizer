namespace FileOrganizer.WebUI.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        public AuthUser? CurrentUser { get; private set; } = null;

        public bool Login( string userName, string password )
        {
            // TODO: FAKE DATA

            if (userName == "admin") // any pass
            {
                CurrentUser = new AuthUser(
                    new UserName( userName ),
                    new UserDisplayName( "administrator" ),
                    new UserRoles( new[] { "administrator", "moderator" } )
                    );

                return true;
            }

            if (userName == "mod") // any pass
            {
                CurrentUser = new AuthUser(
                    new UserName( userName ),
                    new UserDisplayName( "moderator" ),
                    new UserRoles( new[] { "moderator" } )
                    );

                return true;
            }

            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
