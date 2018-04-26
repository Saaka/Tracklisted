using Microsoft.Extensions.DependencyInjection;

namespace Tracklisted.Commands.Sender.Config
{
    public static class ModuleRegistration
    {
        /// <summary>
        /// Register dependencies from message sender module
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommandsSender(this IServiceCollection services)
        {
            services.AddSingleton<IMessageSenderClient, SenderClient>();

            return services;
        }
    }
}
