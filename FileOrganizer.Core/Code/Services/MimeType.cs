namespace FileOrganizer.Core.Services
{
    public sealed class MimeType
    {
        public MimeType( string value )
        {
            Value = value;
        }

        public string Value { get; }

        public static MimeType Unknown { get; } = new MimeType( "application/octet-stream" );
    }
}
