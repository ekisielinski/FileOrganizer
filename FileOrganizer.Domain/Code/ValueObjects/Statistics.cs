using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class Statistics
    {
        public Statistics( int fileCount, int uploadCount, int userCount, DataSize occupiedSpace )
        {
            FileCount     = fileCount;
            UploadCount   = uploadCount;
            UserCount     = userCount;
            OccupiedSpace = occupiedSpace;
            //todo: validation
        }

        //====== public properties

        public int      FileCount     { get; }
        public int      UploadCount   { get; }
        public DataSize OccupiedSpace { get; }
        public int      UserCount     { get; }

        //====== public static properties

        public static Statistics Empty { get; } = new( 0, 0, 0, DataSize.Zero );
    }
}
