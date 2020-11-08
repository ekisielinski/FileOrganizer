using MediatR;
using System.Collections.Generic;

namespace FileOrganizer.Domain
{
    public sealed class GetTagsQuery : IRequest<IReadOnlyList<Tag>>
    {
        // empty
    }
}
