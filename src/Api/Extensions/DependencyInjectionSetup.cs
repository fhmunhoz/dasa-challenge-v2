using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System; 
using Dasa.CrossCutting.IoC;

namespace Dasa.Api.Extensions
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services, configuration);
        }
    }
}