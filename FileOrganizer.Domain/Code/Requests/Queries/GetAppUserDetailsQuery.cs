using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class GetAppUserDetailsQuery : IRequest<AppUserDetails>
    {
        public GetAppUserDetailsQuery( UserName userName )
        {
            UserName = Guard.NotNull( userName, nameof( userName ) );
        }

        public UserName UserName { get; }
    }
}
