namespace FileOrganizer.Core.Services
{
    public sealed class UploadId
    {
        public UploadId( int value )
        {
            Value = value;
        }

        public int Value { get; }
    }
}
