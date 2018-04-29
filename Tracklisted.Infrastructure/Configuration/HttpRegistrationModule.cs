using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Tracklisted.Infrastructure.Configuration
{
    public static class HttpRegistrationModule
    {
        private const string HeaderAccept = "Accept";
        private const string AcceptJson = "application/json";

        /// <summary>
        /// Register http client dependencies from infrastructure module
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterHttpClient<TClient>(this IServiceCollection services)
            where TClient: class
        {
            services
                .AddHttpClient<TClient>(config =>
                {
                    config.DefaultRequestHeaders.Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(AcceptJson));
                })
                .AddTransientHttpErrorPolicy(pb => pb.RetryAsync(2));

            return services;
        }
    }
}
