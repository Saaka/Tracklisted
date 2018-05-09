using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Tracklisted.Infrastructure.Configuration
{
    public static class HttpRegistrationExtensions
    {
        private const string HeaderAccept = "Accept";
        private const string AcceptJson = "application/json";

        /// <summary>
        /// Register http client dependencies from infrastructure module
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterHttpClient<TClient>(this IServiceCollection services)
            where TClient : class
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

        /// <summary>
        /// Register http client dependencies from infrastructure module
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterHttpClient<TClient, TClientImplementation>(this IServiceCollection services)
           where TClient : class
           where TClientImplementation : class, TClient
        {
            services
                .AddHttpClient<TClient, TClientImplementation>(config =>
                {
                    config.DefaultRequestHeaders.Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(AcceptJson));
                })
                .AddTransientHttpErrorPolicy(pb => pb.RetryAsync(2));

            return services;
        }
    }
}
