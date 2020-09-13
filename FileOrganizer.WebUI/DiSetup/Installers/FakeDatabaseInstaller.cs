using FileOrganizer.Core;
using FileOrganizer.Core.FakeDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class FakeDatabaseInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<FakeDatabaseSingleton>();

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
        }
    }
}
