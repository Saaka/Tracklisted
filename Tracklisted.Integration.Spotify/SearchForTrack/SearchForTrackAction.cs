using System;
using System.Threading.Tasks;
using Tracklisted.Infrastructure.Actions;
using Tracklisted.Integration.Spotify.SearchForTrack.Models;

namespace Tracklisted.Integration.Spotify.SearchForTrack
{
    public interface ISearchForTrackAction
    {
        Task<SearchForTrackResponse> Execute(SearchForTrackRequest request);
    }
    public class SearchForTrackAction : BaseHttpAction<SearchForTrackResponseWrapper>, ISearchForTrackAction
    {
        private readonly SpotifyApiClient spotifyApiClient;

        private const string MethodName = "search";
        private const int MaximumTracksPerRequest = 50;

        public SearchForTrackAction(SpotifyApiClient spotifyApiClient)
        {
            this.spotifyApiClient = spotifyApiClient;
        }

        public async Task<SearchForTrackResponse> Execute(SearchForTrackRequest request)
        {
            string requestUrl = $"{MethodName}?type=track&offset={request.Offset}&limit={request.Limit}&market={request.Market}&q={CreateQuery(request)}";
            var httpResponse = await spotifyApiClient.CallGetMethod(request, requestUrl);
            var result = await GetSerializedResponse(httpResponse);

            return result?.Response;
        }

        private string CreateQuery(SearchForTrackRequest request)
        {
            return Uri.EscapeDataString(request.TrackName);
        }
    }
}
