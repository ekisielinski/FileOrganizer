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
        {
            this.context = Guard.NotNull( context, nameof( context ) );
        }

        //====== IRequestHandler

        public async Task<IReadOnlyList<ActivityLogEntry>> Handle( GetActivityLogEntriesQuery request, CancellationToken cancellationToken )
        {
            List<ActivityLogEntryEntity>? logs = null;

            if (request.UserNameFilter is null)
            {
                logs = await context.Entities
                    .ActivityLog
                    .Include( x => x.Issuer )
                    .OrderByDescending( x => x.UtcTimestamp )
                    .ToListAsync();

                return logs.Select( MappingUtils.ToActivityLogEntry ).ToList();
            }

            logs = await context.Entities
                .ActivityLog
                .Include( x=> x.Issuer )
                .Where( x => x.Issuer!.UserName == request.UserNameFilter.Value )
                .OrderByDescending( x => x.UtcTimestamp )
                .ToListAsync();

            return logs.Select( MappingUtils.ToActivityLogEntry ).ToList();
        }
    }
}
