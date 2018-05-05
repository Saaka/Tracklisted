using System.Threading.Tasks;
using Tracklisted.Integration.Lastfm.Base;
using Tracklisted.Integration.Lastfm.GetUserTopTracks.Models;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks
{
    public class GetUserTopTracksAction : BaseAction<GetUserTopTracksRequest, GetUserTopTracksResponse>
    {
        private readonly IPeriodMapper _periodMapper;
        private const string MethodName = "user.gettoptracks";

        public GetUserTopTracksAction(
            LastfmApiClient apiClient,
            IPeriodMapper periodMapper) 
            : base(apiClient)
        {
            _periodMapper = periodMapper;
        }

        public override async Task<GetUserTopTracksResponse> Execute(GetUserTopTracksRequest request)
        {
            string requestUrl = $"method={MethodName}&user={request.User}&period={_periodMapper.GetPeriod(request.Period)}";
            var httpResponse = await _apiClient.CallGetMethod(request, requestUrl);
            var result = await GetSerializedResponse(httpResponse);

            return result;
        }
    }
}
