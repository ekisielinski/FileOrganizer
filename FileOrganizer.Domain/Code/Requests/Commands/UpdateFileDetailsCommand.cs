using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class UpdateFileDetailsCommand : IRequest
    {
        public UpdateFileDetailsCommand( FileId fileId, FileTitle? fileTitle, FileDescription? description, PartialDateTime? primaryDateTime)
        {
            FileId = Guard.NotNull( fileId, nameof( fileId ) );

            FileTitle       = fileTitle;
            Description     = description;
            PrimaryDateTime = primaryDateTime;
        }

        //====== public properties

        public FileId           FileId          { get; }
        public FileTitle?       FileTitle       { get; }
        public FileDescription? Description     { get; }
        public PartialDateTime? PrimaryDateTime { get; }

        public bool CanSkipExecution => FileTitle is null && Description is null && PrimaryDateTime is null;
    }
}
