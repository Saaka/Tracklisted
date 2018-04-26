using System.Threading.Tasks;

namespace Tracklisted.Messages.Sender
{
    public interface IMessageSenderClient
    {
        Task SendMessage<T>(T messageBody) where T : IMessage;
    }
}
