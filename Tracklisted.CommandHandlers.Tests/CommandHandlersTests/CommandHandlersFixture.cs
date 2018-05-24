using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Configuration;
using Tracklisted.DAL.Configuration;
using Tracklisted.Integration.Lastfm.Configuration;
using Tracklisted.Integration.Spotify.Configuration;
using Xunit;

namespace Tracklisted.Tests.CommandHandlersTests
{
    public class CommandHandlersFixture : BaseFixture
    {
        public const string CommandHandlersFixtureCollectionDefinition = "Command Handlers Fixture Collection Definition";

        protected override IServiceCollection RegisterDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .RegisterDataModule(configuration)
                .RegisterLastfmModule(configuration)
                .RegisterSpotifyModule()
                .RegisterTracklistedCommandHandlersModule();

            return serviceCollection;
        }
    }

    [CollectionDefinition(CommandHandlersFixture.CommandHandlersFixtureCollectionDefinition)]
    public class SpotifyFixtureCollection : ICollectionFixture<CommandHandlersFixture>
    { }
}
