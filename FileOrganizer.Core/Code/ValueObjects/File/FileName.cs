namespace FileOrganizer.Core
{
    public sealed class FileName
    {
        public FileName( string value )
        {
            Value = value;
        }

        //====== public properties

        public string Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
