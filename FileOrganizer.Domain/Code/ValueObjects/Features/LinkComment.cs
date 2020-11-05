namespace FileOrganizer.Domain
{
    public sealed class LinkComment
    {
        public LinkComment( string value )
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => Value;
    }
}
