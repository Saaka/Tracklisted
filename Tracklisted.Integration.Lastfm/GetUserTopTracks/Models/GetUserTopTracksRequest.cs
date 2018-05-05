using Tracklisted.Integration.Lastfm.Base;
using Tracklisted.Integration.Lastfm.Base.Request;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks.Models
{
    public class GetUserTopTracksRequest : IPageable
    {
        public GetUserTopTracksRequest()
        {
            Limit = 50;
            Page = 1;
            Period = PeriodType.Overall;
        }
        public string User { get; set; }
        public PeriodType Period { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
    }
}
