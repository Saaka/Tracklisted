using Newtonsoft.Json;
using System.Collections.Generic;
using Tracklisted.Integration.Lastfm.Base.Response;
using Tracklisted.Integration.Lastfm.Models;

namespace Tracklisted.Integration.Lastfm.GetUserTopTracks.Models
{
    public class UserTopTrack : IHasImage
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }
        public int Rank { get; set; }
        [JsonProperty("mbid")]
        public string MusicBrainzID { get; set; }
        [JsonProperty("url")]
        public string LastfmUrl { get; set; }

        [JsonProperty("artist")]
        public ArtistSimple Artist { get; set; }
        [JsonProperty("image")]
        public List<ImageInfo> Images { get; set; }
    }
}
