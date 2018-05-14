using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tracklisted.Commands.GetUserTopTracksList;
using Tracklisted.Commands.Sender;

namespace Tracklisted.WebAPI.Controllers.UserTopTracks
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTopTracksController : ControllerBase
    {
        private readonly IMessageSenderClient _senderClient;
        private readonly ILogger<UserTopTracksController> _logger;

        public UserTopTracksController(IMessageSenderClient senderClient,
            ILogger<UserTopTracksController> logger)
        {
            _senderClient = senderClient;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string user)
        {
            string messageId = Guid.NewGuid().ToString();

            _logger.LogInformation($"Created command with id {messageId}");

            await _senderClient
                .Send(new CreateUserTopTracksListCommand { CommandId = messageId, LastfmUserName = user});

            return new JsonResult(messageId);
        }
    }
}
