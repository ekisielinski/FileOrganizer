using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class GetMetadataFromFileContentQuery : IRequest<FileMetadataContainer>
    {
        public GetMetadataFromFileContentQuery( FileId fileId )
        {
            FileId = Guard.NotNull( fileId, nameof( fileId ) );
        }

        //====== public properties

        public FileId FileId { get; }
    }
}
