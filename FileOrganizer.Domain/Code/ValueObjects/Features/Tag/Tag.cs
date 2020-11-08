using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class Tag
    {
        public Tag( TagName name, TagDisplayName displayName, TagDescription description, UtcTimestamp whenCreated )
        {
            Name        = Guard.NotNull( name, nameof( name ) );
            DisplayName = Guard.NotNull( displayName, nameof( displayName ) );
            Description = Guard.NotNull( description, nameof( description ) );
            WhenCreated = Guard.NotNull( whenCreated, nameof( whenCreated ) );
            // todo: created-by
        }

        //====== public properties

        public TagName        Name        { get; }
        public TagDisplayName DisplayName { get; }
        public TagDescription Description { get; }
        public UtcTimestamp   WhenCreated { get; }

        //====== override: object

        public override string ToString() => DisplayName.IsEmpty ? Name.ToString() : $"{Name} - {DisplayName}";
    }
}
