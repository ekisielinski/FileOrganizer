namespace FileOrganizer.CommonUtils
{
    public readonly struct DataSize
    {
        public DataSize( long bytes )
        {
            Bytes = bytes;
        }

        //====== public properties

        public long Bytes { get; }

        //====== override: Object

        public override string ToString() => Bytes.ToString();
    }
}
