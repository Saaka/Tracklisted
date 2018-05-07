using Microsoft.Extensions.Configuration;

namespace Tracklisted.Integration.Spotify.Configuration
{
    public interface ISpotifyConfiguration
    {
        string ClientID { get; }
        string ClientSecret { get; }
        string Url { get; }
        string AuthorizationUrl { get; }
    }

    internal class SpotifyConfiguration : ISpotifyConfiguration
    {
        private readonly IConfiguration _config;

        public SpotifyConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public string ClientID => _config[ConfigurationProperties.Spotify.ClientID].ToString();
        public string ClientSecret => _config[ConfigurationProperties.Spotify.ClientSecret].ToString();
        public string Url => _config[ConfigurationProperties.Spotify.Url].ToString();
        public string AuthorizationUrl => _config[ConfigurationProperties.Spotify.AuthorizationUrl].ToString();
    }

    internal class ConfigurationProperties
    {
        public class Spotify
        {
            public const string ClientID = "Spotify:ClientID";
            public const string ClientSecret = "Spotify:ClientSecret";
            public const string Url = "Spotify:Url";
            public const string AuthorizationUrl = "Spotify:AuthorizationUrl";
        }
    }
}
