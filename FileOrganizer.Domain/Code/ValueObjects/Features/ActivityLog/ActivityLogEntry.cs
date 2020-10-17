using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class ActivityLogEntry
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
