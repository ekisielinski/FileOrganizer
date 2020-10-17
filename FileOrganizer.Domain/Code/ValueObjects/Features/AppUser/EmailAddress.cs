using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class EmailAddress
    {
        public EmailAddress( string value )
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
