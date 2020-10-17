using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class FileDescription
    {
        public FileDescription( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // TODO: validation
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
