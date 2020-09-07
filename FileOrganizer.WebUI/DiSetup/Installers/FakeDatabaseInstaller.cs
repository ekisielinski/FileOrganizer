using FileOrganizer.Core;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class FakeDatabaseInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<FakeDatabaseSingleton>( sp => new FakeDatabaseSingleton(
                sp.GetRequiredService<IFileDatabase>(),
                sp.GetRequiredService<ITimestampGenerator>(),
                sp.GetRequiredService<IThumbnailsMaker>(),
                sp.GetRequiredService<ISha256Generator>(),
                sp.GetRequiredService<IPasswordHasher>()
            ) );

            services.AddTransient<IFileUploader>         ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IFileDetailsReader>    ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IFileDetailsUpdater>   ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<ICredentialsValidator> ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserFinder>        ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserReader>        ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserUpdater>       ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IFileSearcher>         ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IUploadInfoReader>     ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
            services.AddTransient<IAppUserCreator>       ( sp => sp.GetRequiredService<FakeDatabaseSingleton>() );
        }
    }
}
