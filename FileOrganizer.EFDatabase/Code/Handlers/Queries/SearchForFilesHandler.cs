using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class SearchForFilesHandler : IRequestHandler<SearchForFilesQuery, FileSearchResult>
    {
        readonly EFAppContext context;

        //====== ctors

        public SearchForFilesHandler( EFAppContext context )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
        }

        //====== IRequestHandler

        public async Task<FileSearchResult> Handle( SearchForFilesQuery request, CancellationToken cancellationToken )
        {
            int count = await context.Entities.Files.CountAsync(); // TODO: as 1 query?

            List<FileEntity>? entities = await context.Entities
                .Files
                .Skip( request.PagingParameters.SkipCount )
                .Take( request.PagingParameters.PageSize )
                .Include( x => x.Uploader )
                .Include( x => x.Image )
                .AsNoTracking()
                .ToListAsync();

            IReadOnlyList<FileDetails> retrieved = entities.Select( MappingUtils.ToFileDetails ).ToList();

            return new FileSearchResult( retrieved, count, request.PagingParameters );
        }
    }
}
