using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Tracklisted.CommandHandlers.Infrastructure;
using Tracklisted.Commands.GetArtistTopTracks;
using Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure;
using Tracklisted.Integration.Lastfm.GetUserTopTracks;
using Tracklisted.Integration.Lastfm.GetUserTopTracks.Models;

namespace Tracklisted.Commands.Receiver.CommandHandlers
{
    public class GetUserTopTracksCommandHandler : BaseCommandHandler<GetUserTopTracksCommand>
    {
        private readonly ILogger<GetUserTopTracksCommandHandler> _logger;
        private readonly IGetUserTopTracksAction _getUserTopTracks;

        public GetUserTopTracksCommandHandler(ILogger<GetUserTopTracksCommandHandler> logger,
             IGetUserTopTracksAction getUserTopTracks)
        {
            _logger = logger;
            _getUserTopTracks = getUserTopTracks;
        }

        protected override async Task HandleCommand(GetUserTopTracksCommand command)
        {
            _logger.LogInformation($"**** S T A R T **** - Get top tracks for {command.User}");

            var result = await _getUserTopTracks.Execute(new GetUserTopTracksRequest
            {
                User = command.User
            });

            if (result.UserTopTracks?.Tracks?.Any() == true)
            {
                var track = result.UserTopTracks.Tracks.First();
                _logger.LogInformation($"Top track for {command.User} is {track.Name} by {track.Artist.Name}");
            }

            _logger.LogInformation($"**** E N D **** - Get top tracks for {command.User}");
        }
    }
}
