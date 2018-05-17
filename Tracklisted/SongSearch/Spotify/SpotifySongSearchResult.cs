using System.Collections.Generic;
using Tracklisted.Integration.Spotify.Models;

namespace Tracklisted.SongSearch.Spotify
{
    public class SpotifySongSearchResult
    {
        public SpotifySongSearchResult()
        {
            AlternativeTracks = new List<SpotifyTrack>();
        }
        public bool ExactMatch { get; set; }
        public bool TrackAvailable { get; set; }
        public bool HasAlternativeTracks { get; set; }
        public SpotifyTrack Track { get; set; }
        public List<SpotifyTrack> AlternativeTracks { get; set; }
    }
}
