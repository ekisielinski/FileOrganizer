namespace FileOrganizer.Core
{
    public sealed class DirectoryFullPath
    {
        public DirectoryFullPath( string value )
        {
            Value = value;
        }

        //====== public properties

        public string Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
