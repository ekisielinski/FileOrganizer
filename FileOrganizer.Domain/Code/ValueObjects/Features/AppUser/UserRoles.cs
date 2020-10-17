using FileOrganizer.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Domain
{
    public sealed class UserRoles
    {
        public UserRoles( IEnumerable<UserRole> roles )
        {
            Items = ArgUtils.ToRoList( roles, nameof( roles ) )
                            .OrderBy( x => x.Value )
                            .ToList();

            int countDistinct = Items.Select( x => x.Value ).Distinct().Count();
            if (countDistinct < Items.Count) throw new ArgumentException( "Duplicated roles.", nameof( roles ) );

            if (Items.Count > 50) throw new ArgumentException( "Too many roles. Max is 50.", nameof( roles ) );
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
