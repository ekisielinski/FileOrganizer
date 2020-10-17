using MediatR;
using System.Collections.Generic;

namespace FileOrganizer.Domain
{
    public sealed class GetUploadBasicInfosQuery : IRequest<IReadOnlyList<UploadBasicInfo>>
    {
        // TODO: add filters, like: uploads by user, paging
    }
}
