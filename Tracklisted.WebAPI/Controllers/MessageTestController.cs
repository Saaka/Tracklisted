using Microsoft.AspNetCore.Mvc;
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

        public MessageTestController(IMessageSenderClient senderClient)
        {
            _senderClient = senderClient;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostMessage()
        {
            string messageId = Guid.NewGuid()
                .ToString();

            await _senderClient
                .Send(new TestMessage { CommandId = messageId });

            return new JsonResult(messageId);
        }
    }

    internal class TestMessage : ICommand
    {
        public string CommandId { get; set; }
    }
}
