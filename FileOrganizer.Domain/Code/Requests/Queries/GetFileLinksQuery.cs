using FileOrganizer.CommonUtils;
using MediatR;
using System.Collections.Generic;

namespace FileOrganizer.Domain
{
    public sealed class GetFileLinksQuery : IRequest<IReadOnlyList<FileLink>>
    {
        public GetFileLinksQuery( FileId fileId )
        {
            FileId = Guard.NotNull( fileId, nameof( fileId ) );
        }

        //====== public properties

        public FileId FileId { get; }
    }
}
