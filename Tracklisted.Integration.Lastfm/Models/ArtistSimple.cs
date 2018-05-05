using Newtonsoft.Json;

namespace Tracklisted.Integration.Lastfm.Models
{
    public class ArtistSimple
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("mbid")]
        public string MusicBrainzID { get; set; }
        [JsonProperty("url")]
        public string LastfmUrl { get; set; }
    }
}
