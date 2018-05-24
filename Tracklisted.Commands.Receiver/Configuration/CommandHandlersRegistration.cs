using Microsoft.Extensions.DependencyInjection;
using Tracklisted.CommandHandlers.CreateUserTopTracksList;
using Tracklisted.Commands.Receiver.CommandHandlers;
using Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure;

namespace Tracklisted.Commands.Receiver.Configuration
{
    public static class CommandHandlersRegistration
    {
        public static IServiceCollection RegisterCommandHandlers(this IServiceCollection services)
        {
            services
                .AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services
                .AddScoped<ICommandBodyMapper, CommandBodyMapper>();

            services
                .AddTransient<GetArtistTopTracksCommandHandler>()
                .AddTransient<GetUserTopTracksCommandHandler>();                

            return services;
        }
    }
}
