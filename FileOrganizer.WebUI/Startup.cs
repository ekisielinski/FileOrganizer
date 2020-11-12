using FileOrganizer.Core;
using FileOrganizer.EFDatabase;
using FileOrganizer.Services.FileDatabase;
using FileOrganizer.WebUI.DiSetup;
using FileOrganizer.WebUI.DiSetup.Installers;
using FileOrganizer.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace FileOrganizer.WebUI
{
    public sealed class Startup
    {
        public Startup( IConfiguration configuration ) => Configuration = configuration;

        //====== public properties

        public IConfiguration Configuration { get; }

        //====== configuration

        public void ConfigureServices( IServiceCollection services )
        {
            //services.AddControllersWithViews(); // TODO: when it is required?

            services.AddRazorPages( options =>
            {
                options.Conventions.AuthorizeFolder( "/" );
            } ).AddFluentValidation( cfg => cfg.RegisterValidatorsFromAssemblyContaining<Startup>() );

            services.AddFeatureManagement();
            services.AddHttpContextAccessor();
            services.AddTransient<IStaticFilesLinkGenerator, StaticFilesLinkGenerator>();
            services.AddTransient<IDatabaseInitializer, DefaultDatabaseInitializer>();
            services.AddTransient<IRequestorAccessor, RequestorAccessor>();

            ServicesInstallerHelper.InstallAll( services, Configuration, typeof( Startup ).Assembly );
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints( ep =>
            {
                ep.MapControllerRoute( name: "default", pattern: "{controller}/{action=Index}/{id?}" );
                ep.MapRazorPages();
            } );

            using IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FileOrganizerEntities>();
                bool created = dbContext.Database.EnsureCreated();

                if (created)
                {
                    var cleaner = serviceScope.ServiceProvider.GetRequiredService<IFileDatabaseCleaner>();
                    cleaner.DeleteAllFiles();

                    RequestorAccessor.UseAdmin = true;
                    serviceScope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().Init();
                    RequestorAccessor.UseAdmin = false;
                }
            }
        }
    }
}
