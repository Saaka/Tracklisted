using System.Threading.Tasks;

namespace Tracklisted.Commands.Sender
{
    public interface IMessageSenderClient
    {
        Task Send<T>(T message) where T : BaseCommand;
    }
}
