using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class Statistics
    {
        public Statistics( int fileCount, int uploadCount, int userCount, DataSize occupiedSpace )
        {
            FileCount = fileCount;
            UploadCount = uploadCount;
            UserCount = userCount;
            OccupiedSpace = occupiedSpace;
            //todo: validation
        }

        public int FileCount { get; }
        public int UploadCount { get; }
        public DataSize OccupiedSpace { get; }
        public int UserCount { get; }

        public static Statistics Empty { get; } = new Statistics( 0, 0, 0, DataSize.Zero );
    }
}
