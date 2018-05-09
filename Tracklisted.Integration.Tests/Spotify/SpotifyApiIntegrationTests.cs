using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tracklisted.Integration.Spotify.GetSeveralTracks;
using Xunit;

namespace Tracklisted.Integration.Tests.Spotify
{
    [Collection(SpotifyFixture.SpotifyFixtureCollectionDefinition)]
    public class SpotifyApiIntegrationTests
    {
        private readonly SpotifyFixture fixture;

        public SpotifyApiIntegrationTests(SpotifyFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetSeveralTracksTest()
        {
            var action = fixture
                .GetService<IGetSeveralTracksAction>();

            var request = new Integration.Spotify.GetSeveralTracks.Models.GetSeveralTracksRequest
            {

            };

            var result = await action.Execute(request);

            Assert.NotNull(result);
        }
    }
}
