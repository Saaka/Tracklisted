using System.Threading.Tasks;

namespace Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure
{
    public abstract class BaseCommandHandler<T> : ICommandHandler
        where T : BaseCommand
    {
        public async Task HandleCommand(BaseCommand command)
        {
            await HandleCommand(command as T);
        }

        protected abstract Task HandleCommand(T command);
    }
}
