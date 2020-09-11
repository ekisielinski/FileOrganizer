using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class UserRole : IUniqueId, IValueObject
    {
        public UserRole( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // TODO: validation
        }

        //====== public properties

        public string Value { get; }

        //====== public static properties

        public static UserRole Administrator { get; } = new UserRole( "administrator" );
        public static UserRole Moderator     { get; } = new UserRole( "moderator" );

        //====== override: Object

        public override string ToString() => Value;
    }
}
