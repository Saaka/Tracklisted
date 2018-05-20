using Newtonsoft.Json;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks.Models
{
    public class LastfmUserTopTracksMetadata
    {
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("perPage")]
        public int PerPage { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
