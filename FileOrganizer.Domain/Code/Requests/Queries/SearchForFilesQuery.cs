using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class SearchForFilesQuery : IRequest<FileSearchResult>
    {
        public SearchForFilesQuery( PagingParameters pagingParameters )
        {
            PagingParameters = Guard.NotNull( pagingParameters, nameof( pagingParameters ) );
        }

        //====== public properties

        public PagingParameters PagingParameters { get; }
    }
}
