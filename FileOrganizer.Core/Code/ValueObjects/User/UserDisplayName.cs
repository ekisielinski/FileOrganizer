using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class UserDisplayName
    {
        public UserDisplayName( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );

            // TODO: needs validation
        }

        //====== public properties

        public string Value { get; }

        public bool IsEmpty => Value.Length == 0;

        //====== public static properties

        public static UserDisplayName None { get; } = new UserDisplayName( string.Empty );

        //====== override: Object

        public override string ToString() => Value;
    }
}
