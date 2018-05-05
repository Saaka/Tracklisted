using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tracklisted.Integration.Lastfm.Models
{
    public class ImageInfo
    {
        [JsonProperty("#text")]
        public string Url { get; set; }
        [JsonProperty("size")]
        [JsonConverter(typeof(StringEnumConverter), true)]
        public ImageSize ImageSize { get; set; }
    }
}
