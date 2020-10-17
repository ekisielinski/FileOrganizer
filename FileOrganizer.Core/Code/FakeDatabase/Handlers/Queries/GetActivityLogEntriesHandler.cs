using FileOrganizer.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetActivityLogEntriesHandler : IRequestHandler<GetActivityLogEntriesQuery, IReadOnlyList<ActivityLogEntry>>
    {
        private readonly FakeDatabaseSingleton database;

        public GetActivityLogEntriesHandler( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        public Task<IReadOnlyList<ActivityLogEntry>> Handle( GetActivityLogEntriesQuery request, CancellationToken cancellationToken )
        {
            if (request.UserNameFilter is null)
            {
                IReadOnlyList<ActivityLogEntry> res = database.Logs.OrderByDescending( x => x.Timestamp.Value ).ToList();
                return Task.FromResult( res );
            }

            IReadOnlyList<ActivityLogEntry> res1 = database.Logs
                .Where(x => x.UserName.Value == request.UserNameFilter.Value)
                .OrderByDescending( x => x.Timestamp.Value )
                .ToList();

            return Task.FromResult( res1 );
        }
    }
}
