using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Configuration;
using Tracklisted.Commands.Sender.Config;

namespace Tracklisted.WebAPI.Config
{
    public static class ApplicationModulesRegistration
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterConfiguration()
                .RegisterCommandsSender();

            return services;
        }
    }
}
