using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class FakeDatabaseInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddTransient<IFileDetailsReader>    ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IFileDetailsUpdater>   ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddSingleton<ICredentialsValidator> ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddSingleton<IAppUserFinder>        ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddSingleton<IAppUserReader>        ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddSingleton<IAppUserUpdater>       ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddSingleton<IFileSearcher>         ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddSingleton<IUploadInfoReader>     ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddSingleton<IAppUserCreator>       ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
        }
    }
}
