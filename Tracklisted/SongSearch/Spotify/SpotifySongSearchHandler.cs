using System;
using System.Linq;
using System.Threading.Tasks;
using Tracklisted.Integration.Spotify.Models;
using Tracklisted.Integration.Spotify.SearchForTrack;
using Tracklisted.Integration.Spotify.SearchForTrack.Models;

namespace Tracklisted.SongSearch.Spotify
{
    public interface ISpotifySongSearchHandler
    {
        Task<SpotifySongSearchResult> SearchForTrack(string trackName, string artistName, string market);
    }

    public class SpotifySongSearchHandler : ISpotifySongSearchHandler
    {
        private readonly ISearchForTrackAction searchForTrackAction;
        private const int TrackLimit = 4;

        public SpotifySongSearchHandler(ISearchForTrackAction searchForTrackAction)
        {
            this.searchForTrackAction = searchForTrackAction;
        }

        public async Task<SpotifySongSearchResult> SearchForTrack(string trackName, string artistName, string martket)
        {
            var trackResult = await GetSpotifyTrack(trackName, artistName, martket, TrackLimit);

            if (trackResult.Tracks.Any())
            {
                var track = trackResult.Tracks.First();
                if (string.Compare(trackName, track.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    return new SpotifySongSearchResult
                    {
                        ExactMatch = true,
                        TrackAvailable = true,
                        Track = track
                    };
                }
                else
                {
                    return new SpotifySongSearchResult
                    {
                        AlternativeTracks = trackResult.Tracks.Skip(1).ToList(),
                        ExactMatch = true,
                        TrackAvailable = true,
                        Track = track
                    };
                }
            }

            return new SpotifySongSearchResult
            {
                TrackAvailable = false
            };
        }

        private async Task<SearchForTrackResponse> GetSpotifyTrack(string trackName, string artistName, string market, int limit)
        {
            return await searchForTrackAction.Execute(new SearchForTrackRequest
            {
                ArtistName = artistName,
                TrackName = trackName,
                Market = market,
                Limit = limit
            });
        }
    }
}
