using FileOrganizer.CommonUtils;
using MediatR;
using System;

namespace FileOrganizer.Domain
{
    public sealed class UpdateAppUserDetailsCommand : IRequest
    {
        public UpdateAppUserDetailsCommand( UserName userName, UserDisplayName? displayName, EmailAddress? emailAddress, bool deleteEmail )
        {
            UserName = Guard.NotNull( userName, nameof( userName ) );

            EmailAddress = emailAddress;
            DeleteEmail  = deleteEmail;
            DisplayName  = displayName;

            if (deleteEmail && emailAddress != null) throw new ArgumentException( "When deleteEmail is set, email address must be set to null." );
        }

        //====== public properties

        public UserName         UserName     { get; }
        public EmailAddress?    EmailAddress { get; }
        public bool             DeleteEmail   { get; }
        public UserDisplayName? DisplayName  { get; }

        public bool CanSkipExecution => EmailAddress is null && DisplayName is null && DeleteEmail == false;
    }
}
