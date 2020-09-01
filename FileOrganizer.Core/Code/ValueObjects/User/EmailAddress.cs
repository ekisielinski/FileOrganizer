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

        //====== override: Object

        public override string ToString() => Value;
    }
}
