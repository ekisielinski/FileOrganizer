using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public class PagingParameters
    {
        public PagingParameters( int pageSize, int pageIndex )
        {
            PageSize  = pageSize;
            PageIndex = Guard.NotNegative( pageIndex, nameof( pageIndex ) );
        }

        //====== public properties

        public int PageSize  { get; }
        public int PageIndex { get; }

        public int SkipCount => PageSize * PageIndex;

        //====== public static properties

        public static PagingParameters AllAtOnce { get; } = new PagingParameters( int.MaxValue, 0 );
    }
}
