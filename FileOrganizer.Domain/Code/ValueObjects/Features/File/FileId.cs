using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class FileId
    {
        public FileId( int value )
        {
            Value = Guard.MinValue( value, 1, nameof( value ) );
        }

        //====== public properties

        public int Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
