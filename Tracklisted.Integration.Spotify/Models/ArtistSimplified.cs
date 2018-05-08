using Newtonsoft.Json;

namespace Tracklisted.Integration.Spotify.Models
{
    public class ArtistSimplified
    {
        [JsonProperty("id")]
        public string SpotifyId { get; set; }
        [JsonProperty("uri")]
        public string SpotifyUri { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("href")]
        public string ArtistUrl { get; set; }
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
    }
}
