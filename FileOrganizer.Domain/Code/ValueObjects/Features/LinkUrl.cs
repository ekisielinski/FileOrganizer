namespace FileOrganizer.Domain
{
    public sealed class LinkUrl
    {
        public LinkUrl( string value )
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => Value;
    }
}
