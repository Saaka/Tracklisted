using System.Threading.Tasks;

namespace Tracklisted.Integration.Lastfm.Base
{
    public interface IAction<TRequest, TResponse>
            where TRequest : class
            where TResponse : class
    {
        Task<TResponse> Execute(TRequest request);
    }
}
