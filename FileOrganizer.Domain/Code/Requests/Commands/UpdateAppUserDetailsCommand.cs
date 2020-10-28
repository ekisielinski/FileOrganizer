using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class UpdateAppUserDetailsCommand : IRequest
    {
        public UpdateAppUserDetailsCommand( UserName userName, UserDisplayName? displayName, DataUpdateBehavior<EmailAddress> emailAddress )
        {
            UserName     = Guard.NotNull( userName,     nameof( userName ) );
            EmailAddress = Guard.NotNull( emailAddress, nameof( emailAddress ) );

            DisplayName  = displayName;
        }

        //====== public properties

        public UserName         UserName     { get; }
        public UserDisplayName? DisplayName  { get; }

        public DataUpdateBehavior<EmailAddress> EmailAddress { get; }

        public bool CanSkipExecution => DisplayName is null && EmailAddress.Ignore;
    }
}
