using System.Linq;
using System.Threading.Tasks;
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

        public SpotifySongSearchHandler(ISearchForTrackAction searchForTrackAction)
        {
            this.searchForTrackAction = searchForTrackAction;
        }

        public async Task<SpotifySongSearchResult> SearchForTrack(string trackName, string artistName, string martket)
        {
            var trackResult = await GetSpotifyTrack(trackName, artistName, martket);

            return new SpotifySongSearchResult
            {
                Track = trackResult.Tracks.FirstOrDefault(),
                AlternativeTracks = trackResult.Tracks.Skip(1).ToList()
            };
        }

        private async Task<SearchForTrackResponse> GetSpotifyTrack(string trackName, string artistName, string market)
        {
            return await searchForTrackAction.Execute(new SearchForTrackRequest
            {
                ArtistName = artistName,
                TrackName = trackName,
                Market = market
            });
        }
    }
}
