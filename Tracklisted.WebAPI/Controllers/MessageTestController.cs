using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tracklisted.Commands;
using Tracklisted.Commands.Sender;

namespace Tracklisted.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageTestController : ControllerBase
    {
        private readonly IMessageSenderClient _senderClient;
        private readonly ILogger<MessageTestController> _logger;

        public MessageTestController(IMessageSenderClient senderClient,
            ILogger<MessageTestController> logger)
        {
            _senderClient = senderClient;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostMessage()
        {   
            string messageId = Guid.NewGuid()
                .ToString();
            _logger.LogInformation($"Created message with id {messageId}");

            await _senderClient
                .Send(new TestCommand { CommandId = messageId, Sample = "Sample" });
            
            return new JsonResult(messageId);
        }
    }

    internal class TestCommand : BaseCommand
    {
        public TestCommand()
        {
            CommandType = CommandType.GetArtist;
        }
        public string Sample { get; set; }
    }
}
