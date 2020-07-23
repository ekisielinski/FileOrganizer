﻿using FileOrganizer.WebUI.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class CookieAuthServicesInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<IAuthService>( sp => new AuthService() );
            services.AddTransient<IAuthUserAccessor>( sp => sp.GetService<IAuthService>() );

            services.AddAuthentication( "CookieAuthentication" )
                    .AddCookie( "CookieAuthentication", config =>
                    {
                        config.Cookie.Name      = "AuthCookie";
                        config.LoginPath        = "/login";
                        config.LogoutPath       = "/logout";
                        config.AccessDeniedPath = "/accessDenied";
                        config.ExpireTimeSpan   = TimeSpan.FromMinutes( 30 );
                    } );
        }
    }
}
