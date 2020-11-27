using MediatR;
using System.Collections.Generic;

namespace FileOrganizer.Domain
{
    public sealed class GetActivityLogEntriesQuery : IRequest<IReadOnlyList<ActivityLogEntry>>
    {
        public UserName? UserNameFilter { get; init; } = null;

        public PagingParameters Paging { get; init; } = PagingParameters.AllAtOnce;
    }
}
