﻿namespace FileOrganizer.CommonUtils
{
    public readonly struct DataSize
    {
        public DataSize( long bytes ) => Bytes = Guard.MinValue( bytes, 0, nameof( bytes ) );

        //====== public properties

        public long Bytes { get; }

        //====== public static properties

        public static DataSize Zero { get; } = new DataSize( 0 );
        public static DataSize KiB  { get; } = new DataSize( 1024 );
        public static DataSize MiB  { get; } = new DataSize( 1024 * 1024 );

        //====== public static methods

        public static DataSize Sum( DataSize first, DataSize second )
            => new DataSize( first.Bytes + second.Bytes );
        
        //====== override: Object

        public override string ToString()
        {
            if (Bytes < KiB.Bytes) return Bytes + " B";
            if (Bytes < MiB.Bytes) return (Bytes / KiB.Bytes) + " KiB";

            return (Bytes / MiB.Bytes) + " MiB";
        }
    }
}
