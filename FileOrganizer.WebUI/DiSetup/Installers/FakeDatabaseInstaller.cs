using FileOrganizer.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class FakeDatabaseInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<FakeDatabaseSingleton>();
            services.AddTransient<FakeDatabaseUploadApi>();

            services.AddTransient<IFileUploader>         ( sp => sp.GetRequiredService<FakeDatabaseUploadApi>() );
            services.AddTransient<IUploadDetailsReader>  ( sp => sp.GetRequiredService<FakeDatabaseUploadApi>() );
            services.AddTransient<IUploadInfoReader>     ( sp => sp.GetRequiredService<FakeDatabaseUploadApi>() );

            services.AddTransient<IFileDetailsReader>    ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IFileDetailsUpdater>   ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<ICredentialsValidator> ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserFinder>        ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserReader>        ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserUpdater>       ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IFileSearcher>         ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserCreator>       ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
        }
    }
}
