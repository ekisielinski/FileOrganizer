namespace FileOrganizer.Core
{
    public sealed class EmailAddress : IValueObject
    {
        public EmailAddress( string value )
        {
            Value = value;
        }

        //====== public properties

        public string Value { get; }

        //====== public static properties

        public static EmailAddress Empty { get; } = new EmailAddress( string.Empty );

        //====== override: Object

        public override string ToString() => Value;
    }
}
