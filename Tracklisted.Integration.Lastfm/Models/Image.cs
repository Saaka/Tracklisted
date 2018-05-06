using Newtonsoft.Json;
using System;

namespace Tracklisted.Integration.Lastfm.Models
{
    public class ImageInfo
    {
        [JsonProperty("#text")]
        public string Url { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
        [JsonIgnore()]
        public ImageSize ImageSize
        {
            get
            {
                if (Enum.TryParse(Size, true, out ImageSize size))
                    return size;

                return ImageSize.Other;
            }
        }
    }
}
