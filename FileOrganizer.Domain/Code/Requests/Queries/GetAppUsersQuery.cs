using MediatR;
using System.Collections.Generic;

namespace FileOrganizer.Domain
{
    public sealed class GetAppUsersQuery : IRequest<IReadOnlyList<AppUser>>
    {
        // todo: add paging
    }
}
