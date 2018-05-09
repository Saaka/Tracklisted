using Newtonsoft.Json;

namespace Tracklisted.Integration.Spotify.SearchForTrack.Models
{
    public class SearchForTrackResponseWrapper
    {
        [JsonProperty("tracks")]
        public SearchForTrackResponse Response { get; set; }
    }
}
