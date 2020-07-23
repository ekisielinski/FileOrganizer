using FileOrganizer.WebUI.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileOrganizer.WebUI.DiSetup.Installers
{
    public sealed class AuthServicesInstaller : IServicesInstaller
    {
        public void Install( IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<IAuthService>( sp => new AuthService() );
            services.AddTransient<IAuthUserAccessor>( sp => sp.GetService<IAuthService>() );
        }
    }
}
