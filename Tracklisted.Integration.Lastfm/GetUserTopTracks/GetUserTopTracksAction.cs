using System.Threading.Tasks;
using Tracklisted.Infrastructure.Actions;
using Tracklisted.Integration.Lastfm.Base;
using Tracklisted.Integration.Lastfm.GetUserTopTracks.Models;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks
{
    public interface IGetUserTopTracksAction
    {
        Task<GetUserTopTracksResponse> Execute(GetUserTopTracksRequest request);
    }
    public class GetUserTopTracksAction : BaseHttpAction<GetUserTopTracksRequest, GetUserTopTracksResponse>, IGetUserTopTracksAction
    {
        private readonly LastfmApiClient apiClient;
        private readonly IPeriodMapper periodMapper;

        private const string MethodName = "user.gettoptracks";

        public GetUserTopTracksAction(
            LastfmApiClient apiClient,
            IPeriodMapper periodMapper)
        {
            this.apiClient = apiClient;
            this.periodMapper = periodMapper;
        }

        public async Task<GetUserTopTracksResponse> Execute(GetUserTopTracksRequest request)
        {
            string requestUrl = $"?method={MethodName}&user={request.User}&period={periodMapper.GetPeriod(request.Period)}";
            var httpResponse = await apiClient.CallGetMethod(request, requestUrl);
            var result = await GetSerializedResponse(httpResponse);

            return result;
        }
    }
}
