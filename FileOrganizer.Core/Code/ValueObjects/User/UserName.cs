using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class UserName : IValueObject
    {
        public UserName( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );

            // TODO: validation
        }

        //====== public properties

        public string Value { get; }

        //====== override: Object

        public override string ToString() => Value;
    }
}
