using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tracklisted.Messages;
using Tracklisted.Messages.Sender;

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

            await _senderClient.SendMessage(new TestMessage { MessageId = messageId });

            return new JsonResult(messageId);
        }
    }

    internal class TestMessage : IMessage
    {
        public string MessageId { get; set; }
    }
}
