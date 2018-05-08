using Newtonsoft.Json;

namespace Tracklisted.Integration.Spotify.Models
{
    public class SpotifyImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("height")]
        public string Height { get; set; }
        [JsonProperty("width")]
        public string Width { get; set; }
    }
}
