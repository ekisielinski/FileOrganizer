using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class AppUserNames
    {
        public AppUserNames( UserName name, UserDisplayName displayName )
        {
            Name = Guard.NotNull( name, nameof( name ) );
            DisplayName = Guard.NotNull( displayName, nameof( displayName ) );
        }

        //====== public properties

        public UserName Name { get; }
        public UserDisplayName DisplayName { get; }

        //====== override: Object

        public override string ToString()
        {
            if (DisplayName != UserDisplayName.None) return $"{DisplayName} ({Name})";

            return Name.ToString();
        }
    }
}
