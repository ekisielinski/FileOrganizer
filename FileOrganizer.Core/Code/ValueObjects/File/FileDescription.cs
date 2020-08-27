namespace FileOrganizer.Core
{
    public sealed class FileDescription : IValueObject
    {
        public FileDescription( string value )
        {
            Value = value;
        }

        //====== public properties

        public string Value { get; }

        public bool IsEmpty => Value.Length == 0;

        //====== public static properties

        public static FileDescription Empty { get; } = new FileDescription( string.Empty );

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
