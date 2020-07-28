namespace FileOrganizer.Core.Services
{
    public sealed class FileId
    {
        public FileId( int value )
        {
            Value = value;
        }

        public int Value { get; }
    }
}
