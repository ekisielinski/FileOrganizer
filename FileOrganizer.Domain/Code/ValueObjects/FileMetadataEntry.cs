using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class FileMetadataEntry
    {
        public FileMetadataEntry( string name, string value )
        {
            Name  = Guard.NotNull( name,  nameof( name ) );
            Value = Guard.NotNull( value, nameof( value ) );
        }

        //====== public properties

        public string Name  { get; }
        public string Value { get; }

        //====== override: Object

        public override string ToString() => $"{Name}: {Value}";
    }
}
