using FileOrganizer.Core;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using FileOrganizer.WebUI.DiSetup;
using FileOrganizer.WebUI.Services;
using FileOrganizer.WebUI.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            //services.AddControllersWithViews(); // TODO: when it is required?

            services.AddRazorPages( options =>
            {
                options.Conventions.AuthorizeFolder( "/" );
            } );

            services.AddHttpContextAccessor();

            services.AddTransient<ITimestampGenerator, SystemTimestampGenerator>();
            services.AddTransient<IFileDetailsReader>( sp => sp.GetRequiredService<InMemoryFileUploader>() );
            services.AddTransient<IFileDetailsUpdater>( sp => sp.GetRequiredService<InMemoryFileUploader>() );
            services.AddSingleton<IAppUserFinder>( sp => sp.GetRequiredService<InMemoryFileUploader>() );
            services.AddSingleton<IFileSearcher>( sp => sp.GetRequiredService<InMemoryFileUploader>() );
            services.AddSingleton<IUploadInfoReader>( sp => sp.GetRequiredService<InMemoryFileUploader>() );
            services.AddSingleton<IAppUserCreator>( sp => sp.GetRequiredService<InMemoryFileUploader>() );
            services.AddTransient<IStaticFilesLinkGenerator, StaticFilesLinkGenerator>();
            services.AddTransient<IThumbnailsMaker, ThumbnailsMaker>();

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
        }
    }
}
