namespace FileOrganizer.Services.FileDatabase
{
    public sealed class FileName
    {
        public FileName( string value )
        {
            Value = value;
        }

        public string Value { get; }
    }
}
