using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core
{
    public sealed class UserRoles : IValueObject
    {
        public UserRoles( IEnumerable<UserRole> roles )
        {
            Items = ArgUtils.ToRoList( roles, nameof( roles ) )
                            .OrderBy( x => x.Value )
                            .ToList();
            // TODO: avoid duplicates, max number of roles
        }

        //====== public properties

        public IReadOnlyList<UserRole> Items { get; }

        public bool IsAdministrator => Items.Any( x => x.Value == UserRole.Administrator.Value );
        public bool IsModerator     => Items.Any( x => x.Value == UserRole.Moderator.Value );

        //===== public static properties

        public static UserRoles Empty { get; } = new UserRoles( Enumerable.Empty<UserRole>() );

        //===== override: Object

        public override string ToString() => string.Join( " | ", Items );
    }
}
