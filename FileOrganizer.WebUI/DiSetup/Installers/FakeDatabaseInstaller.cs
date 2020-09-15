using FileOrganizer.Core;
using FileOrganizer.Core.FakeDatabase;
using FileOrganizer.WebUI.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class FakeDatabaseInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<FakeDatabaseSingleton>();
            services.AddTransient<IRequestorAccessor, RequestorAccessor>();

            // Upload
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IUploadDetailsReader, UploadDetailsReader>();
            services.AddTransient<IUploadInfoReader, UploadInfoReader>();

            // File
            services.AddTransient<IFileDetailsReader, FileDetailsReader>();
            services.AddTransient<IFileDetailsUpdater, FileDetailsUpdater>();
            services.AddTransient<ICredentialsValidator, CredentialsValidator>();
            services.AddTransient<IFileSearcher, FileSearcher>();

            // AppUser
            services.AddTransient<IAppUserFinder, AppUserFinder>();
            services.AddTransient<IAppUserReader, AppUserReader>();
            services.AddTransient<IAppUserUpdater, AppUserUpdater>();
            services.AddTransient<IAppUserCreator, AppUserCreator>();

            // Activity Logger
            services.AddTransient<IActivityLogger, ActivityLogger>();
            services.AddTransient<IActivityLogReader, ActivityLogReader>();
        }
    }

    // todo: move this class somewhere
    public sealed class RequestorAccessor : IRequestorAccessor
    {
        public static bool UseAdmin = false;

        readonly IAuthUserAccessor authUserAccessor;

        public RequestorAccessor( IAuthUserAccessor authUserAccessor ) => this.authUserAccessor = authUserAccessor;

        public UserName UserName
        {
            get
            {
                if (UseAdmin) return new UserName( "admin" );

                UserName? userName = authUserAccessor.CurrentUser?.Name;

                if (userName is null) throw new InvalidOperationException( "Cannot retrieve user name." );

                return userName;
            }
        }

        public UserRoles Roles
        {
            get
            {
                if (UseAdmin) return new UserRoles( new[] { UserRole.Administrator, UserRole.Moderator } );

                return authUserAccessor.CurrentUser?.Roles ?? UserRoles.Empty;
            }
        }
    }
}
