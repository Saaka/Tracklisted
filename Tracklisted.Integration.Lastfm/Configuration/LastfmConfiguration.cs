using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tracklisted.Integration.Lastfm.Configuration
{
    public interface ILastfmConfiguration
    {
        string ApiKey { get; }
        string SharedSecret { get; }
        string Url { get; }
    }

    internal class LastfmConfiguration : ILastfmConfiguration
    {
        private readonly IConfiguration _config;

        public LastfmConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public string ApiKey => _config[ConfigurationProperties.Lastfm.ApiKey].ToString();
        public string SharedSecret => _config[ConfigurationProperties.Lastfm.SharedSecret].ToString();
        public string Url => _config[ConfigurationProperties.Lastfm.Url].ToString();
    }

    internal class ConfigurationProperties
    {
        public class Lastfm
        {
            public const string ApiKey = "LastFm:ApiKey";
            public const string SharedSecret = "LastFm:SharedSecret";
            public const string Url = "LastFm:Url";
        }
    }
}
