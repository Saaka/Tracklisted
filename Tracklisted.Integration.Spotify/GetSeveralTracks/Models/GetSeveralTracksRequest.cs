using System.Collections.Generic;

namespace Tracklisted.Integration.Spotify.GetSeveralTracks.Models
{
    public class GetSeveralTracksRequest
    {
        public GetSeveralTracksRequest()
        {
            TrackIds = new List<string>();
        }

        public List<string> TrackIds { get; set; }
    }
}
