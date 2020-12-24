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
    public sealed class GetActivityLogEntriesHandler : IRequestHandler<GetActivityLogEntriesQuery, IReadOnlyList<ActivityLogEntry>>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetActivityLogEntriesHandler( EFAppContext context )
            => this.context = Guard.NotNull( context, nameof( context ) );

        //====== IRequestHandler

        public async Task<IReadOnlyList<ActivityLogEntry>> Handle( GetActivityLogEntriesQuery request, CancellationToken cancellationToken )
        {
            string? userNameFilter = request.UserNameFilter?.Value;

            List<ActivityLogEntity> logs = await context.Entities
                .ActivityLog
                .AsNoTracking()
                .Include( x => x.Issuer )
                .Where( x => userNameFilter == null || x.Issuer!.UserName == userNameFilter )
                .OrderByDescending( x => x.UtcTimestamp )
                .Skip( request.Paging.SkipCount )
                .Take( request.Paging.PageSize )
                .ToListAsync();

            return logs.Select( MappingUtils.ToActivityLogEntry ).ToList();
        }
    }
}
