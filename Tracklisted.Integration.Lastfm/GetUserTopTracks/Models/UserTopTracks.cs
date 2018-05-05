using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks.Models
{
    public class UserTopTracks
    {
        [JsonProperty("@attr")]
        public UserTopTracksMetadata Metadata { get; set; }
        [JsonProperty("track")]
        public List<UserTopTrack> Tracks { get; set; }
    }
}
