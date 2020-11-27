using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class UpdateFileDetailsCommand : IRequest
    {
        public UpdateFileDetailsCommand( FileId fileId )
        {
            FileId = Guard.NotNull( fileId, nameof( fileId ) );
        }

        //====== public properties

        public FileId           FileId          { get; }

        public FileTitle?       FileTitle       { get; init; }
        public FileDescription? Description     { get; init; }
        public PartialDateTime? PrimaryDateTime { get; init; }

        public bool CanSkipExecution => FileTitle is null && Description is null && PrimaryDateTime is null;
    }
}
