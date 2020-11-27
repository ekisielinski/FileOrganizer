using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed record FileName
    {
        public FileName( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) ); // TODO: more validation
        }

        public string Value { get; }

        public static implicit operator string( FileName fileName ) => Guard.NotNull( fileName, nameof( fileName ) ).Value;

        public override string ToString() => Value.ToString();
    }
}
