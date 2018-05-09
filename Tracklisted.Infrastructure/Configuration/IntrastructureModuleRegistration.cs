using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Infrastructure.HttpClient;

namespace Tracklisted.Infrastructure.Configuration
{
    public static class IntrastructureModuleRegistration
    {
        public static IServiceCollection RegisterHttpClientHelpers(this IServiceCollection services)
        {
            services
                .AddScoped<IJsonResponseContentDeserializer, JsonResponseContentDeserializer>();

            return services;
        }
    }
}
