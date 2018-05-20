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
            var trackResult = await SearchForTrack(trackName, artistName, martket, TrackLimit);

            if (trackResult != null && trackResult.Tracks.Any())
            {
                foreach (var track in trackResult.Tracks)
                {
                    if (string.Compare(trackName, track.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                        return GetSelectedTrackAsResult(trackResult, track);
                }
                return GetFirstTrackAsResult(trackResult);
            }

            return new SpotifySongSearchResult
            {
                TrackAvailable = false
            };
        }

        private static SpotifySongSearchResult GetSelectedTrackAsResult(SearchForTrackResponse trackResult, SpotifyTrack track)
        {
            return new SpotifySongSearchResult
            {
                AlternativeTracks = trackResult.Tracks.Where(x => x != track).ToList(),
                HasAlternativeTracks = trackResult.Tracks.Count > 1,
                ExactMatch = true,
                TrackAvailable = true,
                Track = track
            };
        }

        private static SpotifySongSearchResult GetFirstTrackAsResult(SearchForTrackResponse trackResult)
        {
            return new SpotifySongSearchResult
            {
                AlternativeTracks = trackResult.Tracks.Skip(1).ToList(),
                HasAlternativeTracks = trackResult.Tracks.Count > 1,
                ExactMatch = true,
                TrackAvailable = true,
                Track = trackResult.Tracks.First()
            };
        }

        private async Task<SearchForTrackResponse> SearchForTrack(string trackName, string artistName, string market, int limit)
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
