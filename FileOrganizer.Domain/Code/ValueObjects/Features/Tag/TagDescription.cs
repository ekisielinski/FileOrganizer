using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class TagDescription
    {
        public TagDescription( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // todo: validation
        }

        public string Value { get; }

        public bool IsEmpty => Value.Length == 0;

        public static TagDescription Empty { get; } = new TagDescription( string.Empty );

        public override string ToString() => Value;
    }
}
