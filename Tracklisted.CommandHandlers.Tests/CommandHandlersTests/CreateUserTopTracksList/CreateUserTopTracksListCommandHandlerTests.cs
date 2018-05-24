using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tracklisted.CommandHandlers.CreateUserTopTracksList;
using Tracklisted.Commands.GetUserTopTracksList;
using Xunit;

namespace Tracklisted.Tests.CommandHandlersTests.CreateUserTopTracksList
{
    [Collection(CommandHandlersFixture.CommandHandlersFixtureCollectionDefinition)]
    public class CreateUserTopTracksListCommandHandlerTests
    {
        private readonly CommandHandlersFixture fixture;

        public CreateUserTopTracksListCommandHandlerTests(CommandHandlersFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        [Trait("Category", "CommandHandlers")]
        [Trait("CommandHandlers", "CreateUserTopTracksListCommandHandler")]
        public async Task HandleCreateUserTopTracksList()
        {
            var commandHandler = fixture
                .GetService<CreateUserTopTracksListCommandHandler>();

            var command = new CreateUserTopTracksListCommand
            {
                CommandId = Guid.NewGuid().ToString(),
                Period = Commands.CreateUserTopTracksList.UserTopTracksPeriod.Day7,
                LastfmUserName = "saka2"
            };

            await commandHandler.HandleCommand(command);
        }
    }
}
