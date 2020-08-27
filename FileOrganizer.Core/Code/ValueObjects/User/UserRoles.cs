using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core
{
    public sealed class UserRoles : IValueObject
    {
        public const string Administrator = "administrator";
        public const string Moderator     = "moderator";

        //====== ctors

        public UserRoles( IEnumerable<string> roles )
        {
            Items = roles.Select( x => x.ToLowerInvariant())
                         .OrderBy( x => x)
                         .ToList(); // TODO: needs validation, normalization?
        }

        //====== public properties

        public IReadOnlyList<string> Items { get; }

        public bool IsAdministrator => Items.Contains( Administrator );
        public bool IsModerator     => Items.Contains( Moderator );

        //===== public static properties

        public static UserRoles Empty { get; } = new UserRoles( Enumerable.Empty<string>() );

        //===== override: Object

        public override string ToString() => string.Join( " | ", Items );
    }
}
