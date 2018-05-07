using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracklisted.Infrastructure.Actions;
using Tracklisted.Integration.Spotify.GetSeveralTracks.Models;

namespace Tracklisted.Integration.Spotify.GetSeveralTracks
{
    public interface IGetSeveralTracksAction
    {
        Task<GetSeveralTracksResponse> Execute(GetSeveralTracksRequest request);
    }

    public class GetSeveralTracksAction : BaseHttpAction<GetSeveralTracksRequest, GetSeveralTracksResponse>, IGetSeveralTracksAction
    {
        private readonly SpotifyApiClient spotifyApiClient;

        private const string MethodName = "tracks";
        private const int MaximumTracksPerRequest = 50;

        public GetSeveralTracksAction(SpotifyApiClient spotifyApiClient)
        {
            this.spotifyApiClient = spotifyApiClient;
        }

        public override async Task<GetSeveralTracksResponse> Execute(GetSeveralTracksRequest request)
        {
            string requestUrl = $"{MethodName}?ids={GetTrackIdsString(request.TrackIds)}";
            var httpResponse = await spotifyApiClient.CallGetMethod(request, requestUrl);
            var result = await GetSerializedResponse(httpResponse);

            return result;
        }

        private object GetTrackIdsString(List<string> trackIds)
        {
            return string.Join(',', trackIds.Take(MaximumTracksPerRequest));
        }
    }
}
