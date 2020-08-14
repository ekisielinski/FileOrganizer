﻿using FileOrganizer.Core;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
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
                string contentRootPath = sp.GetRequiredService<IHostEnvironment>().ContentRootPath;
                string databaseRootPath = Path.Combine( contentRootPath, "Database" );

                return new PhysicalFileDatabase( databaseRootPath );
            } );

            services.AddTransient<IFileDatabase>( sp => sp.GetRequiredService<PhysicalFileDatabase>() );
            services.AddTransient<IFileDatabaseReader>( sp => sp.GetRequiredService<PhysicalFileDatabase>() );

            //--- uploader

            services.AddSingleton<InMemoryFileUploader>( sp => new InMemoryFileUploader(
                sp.GetRequiredService<IFileDatabase>(),
                sp.GetRequiredService<ITimestampGenerator>(),
                sp.GetRequiredService<IThumbnailsMaker>()
                ) );

            services.AddTransient<IFileUploader>( sp => sp.GetRequiredService<InMemoryFileUploader>() );
        }
    }
}
