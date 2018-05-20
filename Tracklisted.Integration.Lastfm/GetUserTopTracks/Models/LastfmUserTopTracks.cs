using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks.Models
{
    public class LastfmUserTopTracks
    {
        [JsonProperty("@attr")]
        public LastfmUserTopTracksMetadata Metadata { get; set; }
        [JsonProperty("track")]
        public List<LastfmUserTopTrack> Tracks { get; set; }
    }
}
