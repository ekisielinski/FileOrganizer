namespace FileOrganizer.Core
{
    public sealed class FileTitle : IValueObject
    {
        public FileTitle( string value )
        {
            Value = value;
        }

        //====== public properties

        public string Value { get; }

        public bool IsEmpty => Value.Length == 0;

        //====== public static properties

        public static FileTitle Empty { get; } = new FileTitle( string.Empty );

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
