using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracklisted.CommandHandlers.Infrastructure;
using Tracklisted.Commands.GetUserTopTracksList;
using Tracklisted.Integration.Lastfm.GetUserTopTracks;
using Tracklisted.Integration.Lastfm.GetUserTopTracks.Models;
using Tracklisted.Integration.Spotify.Models;
using Tracklisted.Integration.Spotify.SearchForTrack;

namespace Tracklisted.CommandHandlers.CreateUserTopTracksList
{
    public class CreateUserTopTracksListCommandHandler : BaseCommandHandler<CreateUserTopTracksListCommand>
    {
        private readonly ILogger<CreateUserTopTracksListCommandHandler> logger;
        private readonly IGetUserTopTracksAction getUserTopTracksAction;
        private readonly ISearchForTrackAction searchForTrackAction;

        public CreateUserTopTracksListCommandHandler(ILogger<CreateUserTopTracksListCommandHandler> logger,
            IGetUserTopTracksAction getUserTopTracksAction,
            ISearchForTrackAction searchForTrackAction)
        {
            this.logger = logger;
            this.getUserTopTracksAction = getUserTopTracksAction;
            this.searchForTrackAction = searchForTrackAction;
        }

        protected override async Task HandleCommand(CreateUserTopTracksListCommand command)
        {
            logger.LogInformation($"Create top tracks list for user {command.LastfmUserName} and period {command.Period.ToString()}");

            var topTrackList = await GetTopTrackList(command);
            var tracksData = await GetTracksData(topTrackList);

        }

        private async Task<IEnumerable<TrackData>> GetTracksData(UserTopTracks topTrackList)
        {
            var list = new List<TrackData>();
            foreach (var topTrack in topTrackList.Tracks)
            {
                var trackData = new TrackData
                {
                    LastfmTopTrack = topTrack
                };
                var searchTrackResult = await GetSpotifyTrack(topTrack);
                if (searchTrackResult != null && searchTrackResult.Total > 0)
                    trackData.SpotifyTrack = searchTrackResult.Tracks.First();

                list.Add(trackData);
            }

            return list;
        }

        private async Task<Integration.Spotify.SearchForTrack.Models.SearchForTrackResponse> GetSpotifyTrack(UserTopTrack topTrack)
        {
            return await searchForTrackAction.Execute(new Integration.Spotify.SearchForTrack.Models.SearchForTrackRequest
            {
                ArtistName = topTrack.Artist.Name,
                TrackName = topTrack.Name,
                Market = "pl"
            });
        }

        private async Task<UserTopTracks> GetTopTrackList(CreateUserTopTracksListCommand command)
        {
            var tracksResponse = await getUserTopTracksAction.Execute(new GetUserTopTracksRequest
            {
                User = command.LastfmUserName,
                Period = (Integration.Lastfm.Base.PeriodType)command.Period
            });

            return tracksResponse.UserTopTracks;
        }

        private class TrackData
        {
            public UserTopTrack LastfmTopTrack { get; set; }
            public SpotifyTrack SpotifyTrack { get; set; }
        }
    }

}
