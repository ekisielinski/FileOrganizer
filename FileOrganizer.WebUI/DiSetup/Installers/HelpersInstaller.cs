using FileOrganizer.Core.Helpers;
using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class HelpersInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddTransient<ISha256Generator, Sha256Generator>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IImageResizer, ImageResizer>();
            services.AddTransient<ITimestampGenerator, SystemTimestampGenerator>();
            services.AddTransient<IMetadataReader, MetadataReader>();

            // TODO: move services with dependencies to different folder than services w/o dependencies

            services.AddTransient<IThumbnailMaker>( sp => new ThumbnailMaker(
                sp.GetRequiredService<IImageResizer>(),
                sp.GetRequiredService<IFileDatabase>().Thumbnails
                ) );
        }
    }
}
