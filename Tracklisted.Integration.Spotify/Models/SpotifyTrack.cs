using Newtonsoft.Json;

namespace Tracklisted.Integration.Spotify.Models
{
    public class SpotifyTrack
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
