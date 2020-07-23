using FileOrganizer.WebUI.DiSetup;
using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileOrganizer.WebUI
{
    public class Startup
    {
        public Startup( IConfiguration configuration ) => Configuration = configuration;

        //====== public properties

        public IConfiguration Configuration { get; }

        //====== configuration

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddRazorPages();
            services.AddHttpContextAccessor();

            services.AddSingleton<IAppUserFinder, AppUserFinderMock>();

            ServicesInstallerHelper.InstallAll( services, Configuration, typeof( Startup ).Assembly );
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints( ep =>
            {
                ep.MapGet( "/", async context =>
                {
                    await context.Response.WriteAsync( "Hello World!" );
                } );

                ep.MapRazorPages();
            } );
        }
    }
}
