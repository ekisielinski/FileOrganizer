namespace FileOrganizer.Core
{
    public class PagingParameters
    {
        public PagingParameters( int pageSize, int pageIndex )
        {
            PageSize  = pageSize;
            PageIndex = pageIndex;
        }

        //====== public properties

        public int PageSize  { get; }
        public int PageIndex { get; }

        public int SkipCount => PageSize * PageIndex;
    }
}
