using FileOrganizer.CommonUtils;
using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class SetAppUserRolesCommand : IRequest
    {
        public SetAppUserRolesCommand( UserName userName, UserRoles userRoles )
        {
            UserName  = Guard.NotNull( userName,  nameof( userName  ) );
            UserRoles = Guard.NotNull( userRoles, nameof( userRoles ) );
        }

        //====== public properties

        public UserName  UserName  { get; }
        public UserRoles UserRoles { get; }
    }
}
