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

        //====== public static methods

        public static DataSize Sum( DataSize first, DataSize second )
            => new DataSize( first.Bytes + second.Bytes );

        //====== override: Object

        public override string ToString()
        {
            if (Bytes < 1024) return Bytes + " B";

            return (Bytes / 1024) + " KiB";
        }
    }
}
