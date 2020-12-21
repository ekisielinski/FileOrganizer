using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class AppUser
    {
        public AppUser( UserName name, UserDisplayName displayName, UserRoles roles )
        {
            Name        = Guard.NotNull( name,        nameof( name ) );
            DisplayName = Guard.NotNull( displayName, nameof( displayName ) );
            Roles       = Guard.NotNull( roles,       nameof( roles ) );
        }

        //====== public properties

        public UserName        Name        { get; }
        public UserDisplayName DisplayName { get; }
        public UserRoles       Roles       { get; }

        public AppUserNames ToAppUserNames() => new AppUserNames( Name, DisplayName );

        //====== override: Object

        public override string ToString()
        {
            if (DisplayName != UserDisplayName.Empty) return $"{DisplayName} ({Name})";

            return Name.ToString();
        }
    }
}
