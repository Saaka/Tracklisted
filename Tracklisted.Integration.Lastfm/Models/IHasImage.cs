using System.Collections.Generic;
using Tracklisted.Integration.Lastfm.Models.Base;

namespace Tracklisted.Integration.Lastfm.Models
{
    public interface IHasImage
    {
        List<ImageInfo> Images { get; set; }
    }
}
