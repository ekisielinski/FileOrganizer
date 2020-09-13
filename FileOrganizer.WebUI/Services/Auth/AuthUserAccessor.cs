using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FileOrganizer.WebUI.Services.Auth
{
    public sealed class AuthUserAccessor : IAuthUserAccessor
    {
        readonly IHttpContextAccessor httpContextAccessor;

        //====== ctors

        public AuthUserAccessor( IHttpContextAccessor httpContextAccessor )
        {
            this.httpContextAccessor = Guard.NotNull( httpContextAccessor, nameof( httpContextAccessor ) );
        }

        //====== IAuthUserAccessor

        public AuthUser? CurrentUser
        {
            get
            {
                if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false) return null;

                ClaimsIdentity firstClaimsIdentity = httpContextAccessor.HttpContext.User.Identities.First();

                string userName        = firstClaimsIdentity.FindFirst( ClaimTypes.NameIdentifier ).Value;
                string userDisplayName = firstClaimsIdentity.FindFirst( ClaimTypes.Name ).Value;

                IEnumerable<UserRole> roles = firstClaimsIdentity
                    .FindAll( claim => claim.Type == ClaimTypes.Role)
                    .Select( claim => new UserRole( claim.Value ) );

                return new AuthUser(
                    new UserName( userName ),
                    new UserDisplayName( userDisplayName ),
                    new UserRoles( roles )
                    );
            }
        }
    }
}
