using System.Threading.Tasks;

namespace Tracklisted.Infrastructure.Actions
{
    public interface IAction<TRequest, TResponse>
            where TRequest : class
            where TResponse : class
    {
        Task<TResponse> Execute(TRequest request);
    }
}
