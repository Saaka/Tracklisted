using Newtonsoft.Json;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks.Models
{
    public class GetUserTopTracksResponse
    {
        [JsonProperty("toptracks")]
        public LastfmUserTopTracks UserTopTracks { get; set; }
    }
}
