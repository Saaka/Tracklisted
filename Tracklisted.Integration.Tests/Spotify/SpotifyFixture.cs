using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Integration.Spotify.Configuration;
using Xunit;

namespace Tracklisted.Integration.Tests.Spotify
{
    public class SpotifyFixture : BaseFixture
    {
        public const string SpotifyFixtureCollectionDefinition = "Spotify Fixture Collection Definition";

        public SpotifyFixture()
        {
            
        }

        protected override IServiceCollection RegisterDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection
                .RegisterSpotifyModule();

            return serviceCollection;
        }
    }

    [CollectionDefinition(SpotifyFixture.SpotifyFixtureCollectionDefinition)]
    public class SpotifyFixtureCollection : ICollectionFixture<SpotifyFixture>
    { }
}
