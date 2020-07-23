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
            Roles = roles.ToList(); // TODO: needs validation, sorting, normalization?
        }

        //====== public properties

        public IReadOnlyList<string> Roles { get; }

        public bool IsAdministrator => Roles.Contains( Administrator );
        public bool IsModerator     => Roles.Contains( Moderator );

        //===== override: Object

        public override string ToString() => string.Join( " | ", Roles );
    }
}
