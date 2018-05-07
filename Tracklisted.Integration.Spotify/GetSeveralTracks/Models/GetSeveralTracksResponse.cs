using Newtonsoft.Json;
using System.Collections.Generic;
using Tracklisted.Integration.Spotify.Models;

namespace Tracklisted.Integration.Spotify.GetSeveralTracks.Models
{
    public class GetSeveralTracksResponse
    {
        [JsonProperty("tracks")]
        public List<SpotifyTrack> Tracks { get; set; }
    }
}
