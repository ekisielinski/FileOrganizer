namespace FileOrganizer.Domain
{
    public sealed class LinkTitle
    {
        public LinkTitle( string value )
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => Value;
    }
}
