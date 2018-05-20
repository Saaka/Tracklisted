﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracklisted.CommandHandlers.Infrastructure;
using Tracklisted.Commands.GetUserTopTracksList;
using Tracklisted.DAL.UserTopTracksStore;
using Tracklisted.Integration.Lastfm.GetUserTopTracks;
using Tracklisted.Integration.Lastfm.GetUserTopTracks.Models;
using Tracklisted.Integration.Spotify.Models;
using Tracklisted.SongSearch.Spotify;

namespace Tracklisted.CommandHandlers.CreateUserTopTracksList
{
    public class CreateUserTopTracksListCommandHandler : BaseCommandHandler<CreateUserTopTracksListCommand>
    {
        private readonly ILogger<CreateUserTopTracksListCommandHandler> logger;
        private readonly IGetUserTopTracksAction getUserTopTracksAction;
        private readonly ISpotifySongSearchHandler spotifySongSearchHandler;
        private readonly IUserTopTracksRepository topTracksRepository;

        public CreateUserTopTracksListCommandHandler(ILogger<CreateUserTopTracksListCommandHandler> logger,
            IGetUserTopTracksAction getUserTopTracksAction,
            ISpotifySongSearchHandler spotifySongSearchHandler,
            IUserTopTracksRepository topTracksRepository)
        {
            this.logger = logger;
            this.getUserTopTracksAction = getUserTopTracksAction;
            this.spotifySongSearchHandler = spotifySongSearchHandler;
            this.topTracksRepository = topTracksRepository;
        }

        protected override async Task HandleCommand(CreateUserTopTracksListCommand command)
        {
            logger.LogInformation($"START - Create top tracks list for user {command.LastfmUserName} and period {command.Period.ToString()}");

            var topTrackList = await GetTopTrackList(command);
            var tracksData = await GetTracksData(topTrackList);

            await topTracksRepository.Add(CreateModel(command, tracksData));

            logger.LogInformation($"END - Create top tracks list for user {command.LastfmUserName} and period {command.Period.ToString()}");
        }

        private Tracklisted.Model.UserTopTracks.UserTopTracks CreateModel(CreateUserTopTracksListCommand command, IEnumerable<TrackData> tracksData)
        {
            return new Model.UserTopTracks.UserTopTracks
            {
                Id = new Guid(command.CommandId),
                CommandType = command.CommandType.ToString(),
                Period = command.Period.ToString(),
                User = command.LastfmUserName,
                Tracks = tracksData.Select(x => new Tracklisted.Model.UserTopTracks.TrackData
                {
                    AlbumName = x.SpotifyTrack?.Album.Name,
                    SpotifyAlbumUrl = x.SpotifyTrack?.Album.ExternalUrls.SpotifyUrl,
                    ArtistName = x.LastfmTopTrack.Artist.Name,
                    LastfmUrl = x.LastfmTopTrack.LastfmUrl,
                    Rank = x.LastfmTopTrack.Rank,
                    TrackName = x.LastfmTopTrack.Name,
                    SpotifyPreview = x.SpotifyTrack?.PreviewUrl,
                    SpotifyUrl = x.SpotifyTrack?.ExternalUrls.SpotifyUrl,
                    Url = x.LastfmTopTrack.Images.FirstOrDefault()?.Url
                }).ToList()
            };
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
                var searchTrackResult = await spotifySongSearchHandler.SearchForTrack(topTrack.Name, topTrack.Artist.Name, "pl");
                if (searchTrackResult.TrackAvailable)
                    trackData.SpotifyTrack = searchTrackResult.Track;

                list.Add(trackData);
            }

            return list;
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
