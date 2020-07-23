using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileOrganizer.WebUI
{
    public class Startup
    {
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddRazorPages();

            services.AddSingleton<IAuthService>( ( sp ) => new AuthService() );
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

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
