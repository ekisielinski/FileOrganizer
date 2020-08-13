namespace FileOrganizer.Core
{
    public sealed class UploadId
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
