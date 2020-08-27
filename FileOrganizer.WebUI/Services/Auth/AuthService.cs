using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FileOrganizer.WebUI.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        readonly ICredentialsValidator credentialsValidator;
        readonly IHttpContextAccessor httpContextAccessor;

        //====== ctors

        public AuthService( ICredentialsValidator credentialsValidator, IHttpContextAccessor httpContextAccessor )
        {
            this.httpContextAccessor = Guard.NotNull( httpContextAccessor, nameof( httpContextAccessor ) );
            this.credentialsValidator = Guard.NotNull( credentialsValidator, nameof( credentialsValidator ) );
        }

        //====== IAuthService

        public AuthUser? CurrentUser
        {
            get
            {
                if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false) return null;

                ClaimsIdentity firstClaimsIdentity = httpContextAccessor.HttpContext.User.Identities.First();

                string userName        = firstClaimsIdentity.FindFirst( ClaimTypes.NameIdentifier ).Value;
                string userDisplayName = firstClaimsIdentity.FindFirst( ClaimTypes.Name ).Value;

                IEnumerable<string> roles = firstClaimsIdentity
                    .FindAll( claim => claim.Type == ClaimTypes.Role)
                    .Select( claim => claim.Value );

                return new AuthUser(
                    new UserName( userName ),
                    new UserDisplayName( userDisplayName ),
                    new UserRoles( roles )
                    );
            }
        }

        public bool Login( UserName userName, string password )
        {
            Guard.NotNull( userName, nameof( userName ) );
            Guard.NotNull( password, nameof( password ) );

            AppUser? appUser = credentialsValidator.ValidateUser( userName, new UserPassword( password ) );

            if (appUser is null) return false;

            var userClaims = new List<Claim>()
            {
                new Claim( ClaimTypes.NameIdentifier, appUser.Name.Value ),
                new Claim( ClaimTypes.Name, appUser.DisplayName.Value ),
            };

            foreach (string role in appUser.Roles.Items)
            {
                userClaims.Add( new Claim( ClaimTypes.Role, role ) );
            }

            var identity = new ClaimsIdentity( userClaims, "CookieAuthentication" );
            var userPrincipal = new ClaimsPrincipal( new[] { identity } );

            httpContextAccessor.HttpContext.SignInAsync( userPrincipal ); // TODO: async

            return true;
        }

        public void Logout()
        {
            httpContextAccessor.HttpContext.SignOutAsync(); // TODO: async
        }
    }
}
