using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tracklisted.CommandHandlers.Infrastructure;
using Tracklisted.Commands.GetUserTopTracksList;

namespace Tracklisted.CommandHandlers.CreateUserTopTracksList
{
    public class CreateUserTopTracksListCommandHandler : BaseCommandHandler<CreateUserTopTracksListCommand>
    {
        private readonly ILogger<CreateUserTopTracksListCommandHandler> logger;

        public CreateUserTopTracksListCommandHandler(ILogger<CreateUserTopTracksListCommandHandler> logger)
        {
            this.logger = logger;
        }

        protected override async Task HandleCommand(CreateUserTopTracksListCommand command)
        {
            logger.LogInformation($"Create user {command.LastfmUserName} top tracks list");
        }
    }
}
