using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.Domain;

namespace FileOrganizer.WebUI.Services.Auth
{
    public sealed class AuthUser
    {
        public AuthUser( UserName name, UserDisplayName displayName, UserRoles roles )
        {
            Name        = Guard.NotNull( name,        nameof( name ) );
            DisplayName = Guard.NotNull( displayName, nameof( displayName ) );
            Roles       = Guard.NotNull( roles,       nameof( roles ) );
        }

        //====== public properties

        public UserName        Name        { get; }
        public UserDisplayName DisplayName { get; }
        public UserRoles       Roles       { get; }

        //====== override: Object

        public override string ToString()
        {
            if (DisplayName != UserDisplayName.None) return $"{DisplayName} ({Name})";

            return Name.ToString();
        }
    }
}
