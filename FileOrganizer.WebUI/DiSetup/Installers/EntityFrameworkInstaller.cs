using FileOrganizer.EFDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class EntityFrameworkInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddMediatR( typeof( EFAppContext ).Assembly );

            services.AddDbContext<FileOrganizerEntities>( opt => opt.UseSqlServer( configuration["ConnectionString:FileOrganizerDb"] ) );

            services.AddTransient<EFAppContext>();
            services.AddTransient<IActivityLogger, ActivityLogger>();
            services.AddTransient<ICredentialsValidator, CredentialsValidator>();
        }
    }
}
