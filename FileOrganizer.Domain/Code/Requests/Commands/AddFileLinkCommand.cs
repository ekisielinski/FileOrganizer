using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class AddFileLinkCommand : IRequest<LinkId>
    {
        public AddFileLinkCommand( FileId fileId, LinkUrl address, LinkTitle? title, LinkComment? comment )
        {
            FileId  = Guard.NotNull( fileId, nameof( fileId ) );
            Address = Guard.NotNull( address, nameof( address ) );
            Title   = title;
            Comment = comment;
        }

        //====== public properties

        public FileId       FileId  { get; }
        public LinkUrl      Address { get; }
        public LinkTitle?   Title   { get; }
        public LinkComment? Comment { get; }
    }
}
