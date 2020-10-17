using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class MimeType
    {
        public MimeType( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // TODO: validation
        }

        //====== public properties

        public string Value { get; }

        //====== public static properties

        public static MimeType Unknown { get; } = new MimeType( "application/octet-stream" );

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
