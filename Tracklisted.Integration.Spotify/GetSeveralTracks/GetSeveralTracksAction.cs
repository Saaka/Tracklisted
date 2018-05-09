using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracklisted.Infrastructure.HttpClient;
using Tracklisted.Integration.Spotify.GetSeveralTracks.Models;

namespace Tracklisted.Integration.Spotify.GetSeveralTracks
{
    public interface IGetSeveralTracksAction
    {
        Task<GetSeveralTracksResponse> Execute(GetSeveralTracksRequest request);
    }

    public class GetSeveralTracksAction : IGetSeveralTracksAction
    {
        private readonly SpotifyApiClient spotifyApiClient;
        private readonly IJsonResponseContentDeserializer contentDeserializer;
        private const int MaximumTracksPerRequest = 50;

        public GetSeveralTracksAction(SpotifyApiClient spotifyApiClient,
            IJsonResponseContentDeserializer contentDeserializer)
        {
            this.spotifyApiClient = spotifyApiClient;
            this.contentDeserializer = contentDeserializer;
        }

        public async Task<GetSeveralTracksResponse> Execute(GetSeveralTracksRequest request)
        {
            string requestUrl = $"tracks?ids={GetTrackIdsString(request.TrackIds)}";
            var httpResponse = await spotifyApiClient.CallGetMethod(request, requestUrl);

            var result = await contentDeserializer
                .DeserializeContent<GetSeveralTracksResponse>(httpResponse);

            return result;
        }

        private object GetTrackIdsString(List<string> trackIds)
        {
            return string.Join(',', trackIds.Take(MaximumTracksPerRequest));
        }
    }
}
