using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tracklisted.Commands.GetUserTopTracksList;
using Tracklisted.Commands.Sender;
using Tracklisted.Queries.GetUserTopTracksList;
using Tracklisted.WebAPI.ViewModel.UserTopTracks;

namespace Tracklisted.WebAPI.Controllers.UserTopTracks
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTopTracksController : ControllerBase
    {
        private readonly IMessageSenderClient _senderClient;
        private readonly IGetUserTopTracksListQuery _userTopTracksListQuery;
        private readonly ILogger<UserTopTracksController> _logger;

        public UserTopTracksController(IMessageSenderClient senderClient,
            IGetUserTopTracksListQuery userTopTracksListQuery,
            ILogger<UserTopTracksController> logger)
        {
            _senderClient = senderClient;
            _userTopTracksListQuery = userTopTracksListQuery;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateUserTopTracksList request)
        {
            string commandId = Guid.NewGuid().ToString();

            _logger.LogInformation($"*** CreateUserTopTracksList *** CommandId: {commandId}");

            await _senderClient
                .Send(new CreateUserTopTracksListCommand { CommandId = commandId, LastfmUserName = request.UserName, Period = request.Period });

            return new JsonResult(commandId);
        }

        [HttpGet("{commandId}")]
        public async Task<IActionResult> Get(string commandId)
        {
            _logger.LogInformation($"*** GetUserTopTracksList *** CommandId: {commandId}");

            var result = await _userTopTracksListQuery.Get(commandId);

            return new JsonResult(result);
        }
    }
}
