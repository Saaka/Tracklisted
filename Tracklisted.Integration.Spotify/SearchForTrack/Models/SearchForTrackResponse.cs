using Newtonsoft.Json;
using System.Collections.Generic;
using Tracklisted.Integration.Spotify.Models;

namespace Tracklisted.Integration.Spotify.SearchForTrack.Models
{
    public class SearchForTrackResponse
    {
        [JsonProperty("href")]
        public string QueryUrl { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("items")]
        public List<SpotifyTrack> Tracks{ get; set; }
    }
}
