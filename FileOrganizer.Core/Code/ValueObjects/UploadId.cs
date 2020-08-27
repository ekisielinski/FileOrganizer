namespace FileOrganizer.Core
{
    public sealed class UploadId : IEntityId
    {
        public UploadId( int value )
        {
            Value = value;
        }

        //====== public properties

        public int Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
