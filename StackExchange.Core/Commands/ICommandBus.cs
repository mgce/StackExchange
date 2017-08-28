using System.Threading.Tasks;

namespace StackExchange.Core.Commands
{
    public interface ICommandBus
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
