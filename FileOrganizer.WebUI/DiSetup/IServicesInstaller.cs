using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup
{
    public interface IServicesInstaller
    {
        void Install( IServiceCollection services, IConfiguration configuration );
    }
}
