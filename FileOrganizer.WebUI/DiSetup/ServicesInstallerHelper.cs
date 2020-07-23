using FileOrganizer.CommonUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileOrganizer.WebUI.DiSetup
{
    public static class ServicesInstallerHelper
    {
        public static void InstallAll( IServiceCollection services, IConfiguration configuration, Assembly assembly )
        {
            Guard.NotNull( services,      nameof( services ) );
            Guard.NotNull( configuration, nameof( configuration ) );
            Guard.NotNull( assembly,      nameof( assembly ) );

            IReadOnlyList<Type> types = assembly.GetExportedTypes()
                .Where( type => type.IsClass && type.IsAbstract == false)
                .Where( type => type.GetInterfaces().Contains( typeof( IServicesInstaller ) ) )
                .ToList();

            foreach (Type type in types)
            {
                var instance = (IServicesInstaller) Activator.CreateInstance( type )!;

                instance.Install( services, configuration );
            }
        }
    }
}
