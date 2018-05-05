using System.Collections.Generic;
using Tracklisted.Integration.Lastfm.Models.Base;

namespace Tracklisted.Integration.Lastfm.Models.User.GetTopTracks
{
    public class UserTopTrack : IHasImage
    {
        public string Name { get; set; }
        public int PlayCount { get; set; }
        public int Rank { get; set; }
        public string MusicBrainzID { get; set; }
        public string LastfmUrl { get; set; }
        public int MyProperty { get; set; }

        public ArtistSimple Artist { get; set; }
        public List<ImageInfo> Images { get; set; }
    }
}
