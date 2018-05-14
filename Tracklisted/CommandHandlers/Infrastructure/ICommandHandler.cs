using System.Threading.Tasks;
using Tracklisted.Commands;

namespace Tracklisted.CommandHandlers.Infrastructure
{
    public interface ICommandHandler
    {
        Task HandleCommand(BaseCommand command);
    }
}
