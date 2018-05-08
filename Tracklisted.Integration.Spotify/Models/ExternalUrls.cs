using Newtonsoft.Json;

namespace Tracklisted.Integration.Spotify.Models
{
    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string SpotifyUrl { get; set; }
    }
}
