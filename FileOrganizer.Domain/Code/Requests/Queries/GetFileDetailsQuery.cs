using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class GetFileDetailsQuery : IRequest<FileDetails>
    {
        public GetFileDetailsQuery( FileId fileId )
        {
            FileId = Guard.NotNull( fileId, nameof( fileId ) );
        }

        //====== public properties

        public FileId FileId { get; }
    }
}
