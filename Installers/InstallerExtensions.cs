using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                                                  typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                                                          .Select(Activator.CreateInstance) //select and create instance
                                                          .Cast<IInstaller>() //cast the result to the type of IInstaller
                                                          .ToList(); //convert to list

            installers.ForEach(installer => installer.InstallServices(services, configuration)); //iterate through each installer and register them

        }
    }
}
