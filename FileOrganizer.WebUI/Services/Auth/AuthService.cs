using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using FileOrganizer.EFDatabase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        readonly IAuthUserAccessor authUserAccessor;
        readonly ICredentialsValidator credentialsValidator;
        readonly IHttpContextAccessor httpContextAccessor;

        //====== ctors

        public AuthService(
            IAuthUserAccessor authUserAccessor,
            ICredentialsValidator credentialsValidator,
            IHttpContextAccessor httpContextAccessor )
        {
            this.authUserAccessor     = Guard.NotNull( authUserAccessor, nameof( authUserAccessor ) );
            this.httpContextAccessor  = Guard.NotNull( httpContextAccessor, nameof( httpContextAccessor ) );
            this.credentialsValidator = Guard.NotNull( credentialsValidator, nameof( credentialsValidator ) );
        }

        //====== IAuthService

        public AuthUser? CurrentUser => authUserAccessor.CurrentUser;

        public async Task<bool> LoginAsync( UserName userName, string password )
        {
            Guard.NotNull( userName, nameof( userName ) );
            Guard.NotNull( password, nameof( password ) );

            AppUser? appUser = credentialsValidator.TryGetUser( userName, new( password ) );

            if (appUser is null) return false;

            var userClaims = new List<Claim>()
            {
                new Claim( ClaimTypes.NameIdentifier, appUser.Name.Value ),
                new Claim( ClaimTypes.Name, appUser.DisplayName.Value ),
            };

            foreach (UserRole role in appUser.Roles.Items)
            {
                userClaims.Add( new Claim( ClaimTypes.Role, role.Value ) );
            }

            var identity = new ClaimsIdentity( userClaims, "CookieAuthentication" );
            var userPrincipal = new ClaimsPrincipal( new[] { identity } );

            if (httpContextAccessor.HttpContext is null) throw new InvalidOperationException( "HttpContext is not available." );

            await httpContextAccessor.HttpContext.SignInAsync( userPrincipal );

            return true;
        }

        public async Task LogoutAsync()
        {
            if (httpContextAccessor.HttpContext is null) throw new InvalidOperationException( "HttpContext is not available." );

            await httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
