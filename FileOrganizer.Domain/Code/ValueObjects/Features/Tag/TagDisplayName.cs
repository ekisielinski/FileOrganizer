using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class TagDisplayName
    {
        public TagDisplayName( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // todo: validation
        }

        public string Value { get; }

        public bool IsEmpty => Value.Length == 0;

        public static TagDisplayName Empty { get; } = new TagDisplayName( string.Empty );

        public override string ToString() => Value;
    }
}
