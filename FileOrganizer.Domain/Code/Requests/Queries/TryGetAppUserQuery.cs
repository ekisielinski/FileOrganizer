using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class TryGetAppUserQuery : IRequest<AppUser?>
    {
        public TryGetAppUserQuery( UserName userName, UserPassword password )
        {
            UserName = Guard.NotNull( userName, nameof( userName ) );
            Password = Guard.NotNull( password, nameof( password ) );
        }

        //====== public properties

        public UserName UserName { get; }

        public UserPassword Password { get; }
    }
}
