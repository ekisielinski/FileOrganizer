namespace FileOrganizer.Domain
{
    public sealed class LinkId
    {
        public LinkId( int value )
        {
            Value = value;
        }

        public int Value { get; }

        public override string ToString() => Value.ToString();
    }
}
