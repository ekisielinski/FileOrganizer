using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Domain
{
    public sealed class FileSearchResult
    {
        public FileSearchResult( IEnumerable<FileDetails> retrieved, int totalFound, PagingParameters pagingParameters )
        {
            Retrieved        = ArgUtils.ToRoList( retrieved, nameof( retrieved ) );
            TotalFound       = Guard.InRange( totalFound, Retrieved.Count, int.MaxValue, nameof( totalFound ) );
            PagingParameters = Guard.NotNull( pagingParameters, nameof( pagingParameters ) );
        }

        public FileSearchResult( PagingParameters pagingParameters )
            : this( Enumerable.Empty<FileDetails>(), 0, pagingParameters )
        {
            // empty
        }

        //====== public properties

        public IReadOnlyList<FileDetails> Retrieved { get; }

        public int TotalFound { get; }

        public PagingParameters PagingParameters { get; }

        public int PageCount => (TotalFound / PagingParameters.PageSize) + (TotalFound % PagingParameters.PageSize > 0 ? 1 : 0);

        //====== override: Object

        public override string ToString()
            => $"Retrieved: {Retrieved.Count} / {TotalFound} total (in {PageCount} pages). {PagingParameters}";
    }
}
