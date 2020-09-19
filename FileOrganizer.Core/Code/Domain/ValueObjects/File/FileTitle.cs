using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class FileTitle : IValueObject
    {
        public FileTitle( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
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
