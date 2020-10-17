using MediatR;
using System.Collections.Generic;

namespace FileOrganizer.Domain
{
    public sealed class GetActivityLogEntriesQuery : IRequest<IReadOnlyList<ActivityLogEntry>>
    {
        public GetActivityLogEntriesQuery( UserName? userNameFilter )
        {
            UserNameFilter = userNameFilter;
        }

        //====== public properties

        public UserName? UserNameFilter { get; }
    }
}
