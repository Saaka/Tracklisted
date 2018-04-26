using Microsoft.Extensions.DependencyInjection;

namespace Tracklisted.Messages.Sender.Config
{
    public static class ModuleRegistration
    {
        /// <summary>
        /// Register dependencies from message sender module
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterMessageSender(this IServiceCollection services)
        {
            services.AddSingleton<IMessageSenderClient, SenderClient>();

            return services;
        }
    }
}
