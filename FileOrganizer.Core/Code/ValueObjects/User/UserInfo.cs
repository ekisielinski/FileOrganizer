using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class UserInfo
    {
        public UserInfo( UserName name, UserDisplayName displayName )
        {
            Name = Guard.NotNull( name, nameof( name ) );
            DisplayName = Guard.NotNull( displayName, nameof( displayName ) );
        }

        //====== public properties

        public UserName Name { get; }
        public UserDisplayName DisplayName { get; }

        //====== override: Object

        public override string ToString()
            => DisplayName.IsEmpty ? Name.Value : $"{Name} - {DisplayName}";
    }
}
