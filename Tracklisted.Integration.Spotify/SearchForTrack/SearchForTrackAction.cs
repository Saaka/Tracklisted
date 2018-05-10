using System;
using System.Net;
using System.Threading.Tasks;
using Tracklisted.Infrastructure.HttpClient;
using Tracklisted.Integration.Spotify.SearchForTrack.Models;

namespace Tracklisted.Integration.Spotify.SearchForTrack
{
    public interface ISearchForTrackAction
    {
        Task<SearchForTrackResponse> Execute(SearchForTrackRequest request);
    }
    public class SearchForTrackAction : ISearchForTrackAction
    {
        private readonly SpotifyApiClient spotifyApiClient;
        private readonly IJsonResponseContentDeserializer contentDeserializer;

        public SearchForTrackAction(SpotifyApiClient spotifyApiClient,
            IJsonResponseContentDeserializer contentDeserializer)
        {
            this.spotifyApiClient = spotifyApiClient;
            this.contentDeserializer = contentDeserializer;
        }

        public async Task<SearchForTrackResponse> Execute(SearchForTrackRequest request)
        {
            string requestUrl = $"search?type=track&offset={request.Offset}&limit={request.Limit}&market={request.Market}&q={CreateQuery(request)}";
            var httpResponse = await spotifyApiClient.CallGetMethod(request, requestUrl);
            var result = await contentDeserializer
                .DeserializeContent<SearchForTrackResponseWrapper>(httpResponse);

            return result?.Response;
        }

        private string CreateQuery(SearchForTrackRequest request)
        {
            return WebUtility.UrlEncode($"artist:{request.ArtistName} track:{request.TrackName}");
        }
    }
}
