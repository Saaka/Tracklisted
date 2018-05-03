using Microsoft.Extensions.DependencyInjection;
using System;
using Tracklisted.Commands.GetArtistTopTracks;

namespace Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler CreateCommandHandler(CommandType commandType);
    }

    internal class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler CreateCommandHandler(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.GetArtistTopTracks:
                    return _serviceProvider.GetRequiredService<GetArtistTopTracksCommandHandler>();
                default:
                    throw new ArgumentException("Invalid command type");
            }
        }
    }
}
