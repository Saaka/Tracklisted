﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tracklisted.Integration.Spotify.Models
{
    public class SpotifyTrack
    {
        [JsonProperty("id")]
        public string SpotifyId { get; set; }
        [JsonProperty("uri")]
        public string SpotifyUri { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }
        [JsonProperty("href")]
        public string TrackUrl { get; set; }
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
        [JsonProperty("album")]
        public AlbumSimplified Album { get; set; }
        [JsonProperty("artists")]
        public List<ArtistSimplified> Artists { get; set; }
    }
}
