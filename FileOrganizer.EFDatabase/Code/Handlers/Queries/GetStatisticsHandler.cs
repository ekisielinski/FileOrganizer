using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class GetStatisticsHandler : IRequestHandler<GetStatisticsQuery, Statistics>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetStatisticsHandler( EFAppContext context )
            => this.context = Guard.NotNull( context, nameof( context ) );

        //====== IRequestHandler

        public async Task<Statistics> Handle( GetStatisticsQuery request, CancellationToken cancellationToken )
        {
            // todo: performance, should it be done in 1 query?
            int fileCount   = await context.Entities.Files.CountAsync();
            int uploadCount = await context.Entities.Uploads.CountAsync();
            int userCount   = await context.Entities.AppUsers.CountAsync();

            long sumFileSize = await context.Entities.Files.SumAsync( x => x.SourceFileSize );

            return new Statistics( fileCount, uploadCount, userCount, new DataSize( sumFileSize ) );
        }
    }
}
