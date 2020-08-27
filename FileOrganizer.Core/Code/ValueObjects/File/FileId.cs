namespace FileOrganizer.Core
{
    public sealed class FileId : IEntityId
    {
        public FileId( int value )
        {
            Value = value;
        }

        //====== public properties

        public int Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
