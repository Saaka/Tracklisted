using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tracklisted.Integration.Spotify.Models
{
    public class AlbumSimplified
    {
        [JsonProperty("id")]
        public string SpotifyId { get; set; }
        [JsonProperty("uri")]
        public string SpotifyUri { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("album_type")]
        public string AlbumType { get; set; }
        [JsonProperty("href")]
        public string AlbumUrl { get; set; }
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
        [JsonProperty("artists")]
        public List<ArtistSimplified> Artists { get; set; }
        [JsonProperty("images")]
        public List<SpotifyImage> Images { get; set; }
    }
}
