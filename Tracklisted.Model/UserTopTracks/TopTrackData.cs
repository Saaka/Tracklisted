namespace Tracklisted.Model.UserTopTracks
{
    public class TopTrackData
    {
        public string ArtistName { get; set; }
        public string TrackName { get; set; }
        public string AlbumName { get; set; }
        public int Rank { get; set; }
        public string SpotifyPreview { get; set; }
        public string SpotifyUrl { get; set; }
        public string SpotifyAlbumUrl { get; set; }
        public string LastfmUrl { get; set; }
        public TopTrackImages Images { get; set; }
    }
}