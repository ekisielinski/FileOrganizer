using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed record FileId
    {
        public FileId( int value ) => Value = Guard.MinValue( value, 1, nameof( value ) );

        public int Value { get; }

        public static implicit operator int( FileId fileId ) => Guard.NotNull( fileId, nameof( fileId ) ).Value;

        public override string ToString() => Value.ToString();
    }
}
