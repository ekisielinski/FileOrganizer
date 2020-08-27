namespace FileOrganizer.Core.Services
{
    public sealed class MimeType : IValueObject
    {
        public MimeType( string value )
        {
            Value = value;
        }

        //====== public properties

        public string Value { get; }

        //====== public static properties

        public static MimeType Unknown { get; } = new MimeType( "application/octet-stream" );

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
