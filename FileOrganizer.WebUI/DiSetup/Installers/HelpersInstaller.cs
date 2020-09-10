using FileOrganizer.Core.Helpers;
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
            services.AddTransient<IThumbnailMaker, ThumbnailMaker>();
            services.AddTransient<ITimestampGenerator, SystemTimestampGenerator>();
        }
    }
}
