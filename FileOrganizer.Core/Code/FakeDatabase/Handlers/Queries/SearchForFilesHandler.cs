using FileOrganizer.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class SearchForFilesHandler : IRequestHandler<SearchForFilesQuery, FileSearchResult>
    {
        readonly FakeDatabaseSingleton database;
        readonly IMediator mediator;

        //====== ctors

        public SearchForFilesHandler( FakeDatabaseSingleton database, IMediator mediator )
        {
            this.database = database;
            this.mediator = mediator;
        }

        public async Task<FileSearchResult> Handle( SearchForFilesQuery request, CancellationToken cancellationToken )
        {
            var filesFound = database.Files
                .Select( x => GetFileDetails( new FileId( x.Id ) ) )
                .ToList();

            var retrieved = filesFound
                .Skip( request.PagingParameters.SkipCount )
                .Take( request.PagingParameters.PageSize );

            var result = new FileSearchResult( retrieved, filesFound.Count, request.PagingParameters );

            return await Task.FromResult( result );
        }

        private FileDetails GetFileDetails( FileId fileId )
        {
            return mediator.Send( new GetFileDetailsQuery( fileId ) ).Result; // todo: result, remove calling command
        }
    }
}
