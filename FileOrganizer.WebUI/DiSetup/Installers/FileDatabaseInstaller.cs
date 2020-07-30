using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class FileDatabaseInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<PhysicalFileDatabase>( sp =>
            {
                string contentRootPath = sp.GetService<IHostEnvironment>().ContentRootPath;
                string databaseRootPath = Path.Combine( contentRootPath, "Database" );

                return new PhysicalFileDatabase( databaseRootPath );
            } );

            services.AddScoped<IFileDatabase>( sp => sp.GetService<PhysicalFileDatabase>() );
            services.AddScoped<IFileDatabaseReader>( sp => sp.GetService<PhysicalFileDatabase>() );
        }
    }
}
