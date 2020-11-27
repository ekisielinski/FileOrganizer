using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed record FileTitle
    {
        public FileTitle( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) ); // TODO: more validation
        }

        public string Value { get; }

        public bool IsEmpty => Value.Length == 0;

        public static FileTitle Empty { get; } = new( string.Empty );

        public static implicit operator string( FileTitle fileTitle ) => Guard.NotNull( fileTitle, nameof( fileTitle ) ).Value;

        public override string ToString() => Value;
    }
}
