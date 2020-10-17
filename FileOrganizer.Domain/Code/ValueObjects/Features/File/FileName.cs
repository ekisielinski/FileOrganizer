using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class FileName
    {
        public FileName( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // TODO: validation
        }

        //====== public properties

        public string Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
