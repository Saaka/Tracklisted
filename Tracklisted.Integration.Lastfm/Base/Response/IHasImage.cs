using System.Collections.Generic;
using Tracklisted.Integration.Lastfm.Models;

namespace Tracklisted.Integration.Lastfm.Base.Response
{
    public interface IHasImage
    {
        List<ImageInfo> Images { get; set; }
    }
}
