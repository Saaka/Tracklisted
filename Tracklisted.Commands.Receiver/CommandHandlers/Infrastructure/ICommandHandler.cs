using System.Threading.Tasks;

namespace Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure
{
    public interface ICommandHandler
    {
        Task HandleCommand(BaseCommand command);
    }
}
