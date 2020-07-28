using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.WebUI.Services.Auth
{
    public sealed class UserRoles
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

        //===== override: Object

        public override string ToString() => string.Join( " | ", Items );
    }
}
