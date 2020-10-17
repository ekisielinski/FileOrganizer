using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class AppUserDetails
    {
        public AppUserDetails( AppUser user, EmailAddress? email, UtcTimestamp whenCreated )
        {
            User        = Guard.NotNull( user, nameof( user ) );
            Email       = email;
            WhenCreated = Guard.NotNull( whenCreated, nameof( whenCreated ) );
        }

        //====== public properties

        public AppUser       User        { get; }
        public EmailAddress? Email       { get; }
        public UtcTimestamp  WhenCreated { get; }

        public bool HasEmail => Email != null;
    }
}
