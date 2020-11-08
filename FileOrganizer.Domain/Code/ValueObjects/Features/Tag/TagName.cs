using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class TagName
    {
        public TagName( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // todo: validation, cannot be empty
        }

        public string Value { get; }

        public override string ToString() => Value;
    }
}
