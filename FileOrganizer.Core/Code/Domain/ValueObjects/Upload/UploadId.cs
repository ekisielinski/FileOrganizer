namespace FileOrganizer.Core
{
    public sealed class UploadId : IUniqueId
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
