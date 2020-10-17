using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class CreateAppUserCommand : IRequest
    {
        public CreateAppUserCommand( UserName userName, UserDisplayName displayName, UserPassword userPassword )
        {
            UserName     = Guard.NotNull( userName,     nameof( userName ) );
            DisplayName  = Guard.NotNull( displayName,  nameof( displayName ) );
            UserPassword = Guard.NotNull( userPassword, nameof( userPassword ) );
        }

        //====== public properties

        public UserName        UserName     { get; }
        public UserDisplayName DisplayName  { get; }
        public UserPassword    UserPassword { get; }
    }
}
