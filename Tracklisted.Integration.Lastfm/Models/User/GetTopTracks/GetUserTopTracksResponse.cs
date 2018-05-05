using System.Collections.Generic;

namespace Tracklisted.Integration.Lastfm.Models.User.GetTopTracks
{
    public class GetUserTopTracksResponse
    {
        public string User { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public int Total { get; set; }

        public List<UserTopTrack> Tracks { get; set; }
    }
}
