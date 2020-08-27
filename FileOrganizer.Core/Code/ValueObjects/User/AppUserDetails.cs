using FileOrganizer.CommonUtils;

namespace FileOrganizer.Core
{
    public sealed class AppUserDetails
    {
        public AppUserDetails( AppUser user, EmailAddress email, UtcTimestamp whenCreated )
        {
            User        = Guard.NotNull( user,        nameof( user ) );
            Email       = Guard.NotNull( email,       nameof( email ) );
            WhenCreated = Guard.NotNull( whenCreated, nameof( whenCreated ) );
        }

        //====== public properties

        public AppUser      User        { get; }
        public EmailAddress Email       { get; }
        public UtcTimestamp WhenCreated { get; }
    }
}
