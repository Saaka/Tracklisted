using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tracklisted.Commands.GetArtistTopTracks;
using Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure;

namespace Tracklisted.Commands.Receiver.CommandHandlers
{
    public class GetArtistTopTracksCommandHandler : BaseCommandHandler<GetArtistTopTracksCommand>
    {
        private readonly ILogger<GetArtistTopTracksCommandHandler> _logger;

        public GetArtistTopTracksCommandHandler(ILogger<GetArtistTopTracksCommandHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task HandleCommand(GetArtistTopTracksCommand command)
        {
            _logger.LogInformation($"Get top tracks for {command.ArtistName}");
        }
    }
}
