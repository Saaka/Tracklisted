namespace Tracklisted.Integration.Spotify.SearchForTrack.Models
{
    public class SearchForTrackRequest
    {
        public SearchForTrackRequest()
        {
            Offset = 0;
            Limit = 5;
        }

        public string TrackName { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string Market { get; set; }
    }
}
