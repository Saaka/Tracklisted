using System.Collections.Generic;
using Tracklisted.Integration.Spotify.Models;

namespace Tracklisted.SongSearch.Spotify
{
    public class SpotifySongSearchResult
    {
        public SpotifyTrack Track { get; set; }
        public List<SpotifyTrack> AlternativeTracks { get; set; }
    }
}
