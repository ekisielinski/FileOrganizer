using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class ActivityLogEntry : IValueObject
    {
        public ActivityLogEntry( UserName userName, UtcTimestamp timestamp, string message )
        {
            UserName  = Guard.NotNull( userName,  nameof( userName ) );
            Timestamp = Guard.NotNull( timestamp, nameof( timestamp ) );
            Message   = Guard.NotNull( message,   nameof( message ) );
        }

        //====== public properties

        public UserName     UserName  { get; }
        public UtcTimestamp Timestamp { get; }
        public string       Message   { get; }
    }
}
