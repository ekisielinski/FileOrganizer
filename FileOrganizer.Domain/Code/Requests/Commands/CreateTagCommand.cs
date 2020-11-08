using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class CreateTagCommand : IRequest
    {
        public CreateTagCommand( TagName tagName, TagDisplayName displayName, TagDescription description ) 
        {
            TagName     = Guard.NotNull( tagName,     nameof( tagName ) );
            DisplayName = Guard.NotNull( displayName, nameof( displayName ) );
            Description = Guard.NotNull( description, nameof( description ) );
        }

        //====== public properties

        public TagName        TagName     { get; }
        public TagDisplayName DisplayName { get; }
        public TagDescription Description { get; }
    }
}
